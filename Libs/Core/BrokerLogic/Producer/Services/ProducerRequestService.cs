using System.Collections.Concurrent;
using System.Net;
using System.Text;
using CoreLib.HttpServiceV2.Services.Interfaces;
using ExampleCore.HttpLogic.Services;
using ExampleCore.TraceLogic.Interfaces;
using MassTransit;
using Medo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.BrokerLogic.Services;

public class ProducerRequestService: IHttpRequestService
{
    
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly string replyQueueName;
    
    private readonly IEnumerable<ITraceWriter> _traceWriterList;
    //private readonly IClientFactory _clientFactory;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();
    public ProducerRequestService( IEnumerable<ITraceWriter> traceWriterList)
    {
    //    _clientFactory = clientFactory;
        _traceWriterList = traceWriterList;
        var factory = new ConnectionFactory 
        {        
            HostName = "localhost",
            Password = "guest",
            UserName = "guest" 
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        
        replyQueueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };
        
        channel.BasicConsume(consumer: consumer,
            queue: replyQueueName,
            autoAck: true);
        
    }

    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse, TRequest>(HttpRequestData requestData,
        HttpConnectionData connectionData = default) where TRequest : class
        where TResponse : class
    {
        
       var props = channel.CreateBasicProperties();
       var correlationId = new Uuid7().ToString();

       props.CorrelationId = correlationId;
       props.ReplyTo = replyQueueName;


       foreach (var traceWriterList in _traceWriterList)
       {
           props.Headers.Add(traceWriterList.Name,traceWriterList.GetValue());
       }

       var json = JsonConvert.SerializeObject(requestData.Body);
       var messegeBytes = Encoding.UTF8.GetBytes(json);
       
       
       var tcs = new TaskCompletionSource<string>();
       callbackMapper.TryAdd(correlationId, tcs);
       
       channel.BasicPublish(exchange: String.Empty,
           routingKey: "rpc/"+requestData.Uri,
           basicProperties: props,
           body: messegeBytes);

       connectionData.CancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));

       var ans = await tcs.Task;
       var res = JsonConvert.DeserializeObject<TResponse>(ans);

       return new HttpResponse<TResponse>()
       {
           Body = res,
           StatusCode = HttpStatusCode.OK
       };
    }
}