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
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.BrokerLogic.Services;

public class ProducerRequestService: IHttpRequestService
{
    private readonly string replyQueueName;
    
    private readonly IEnumerable<ITraceWriter> _traceWriterList;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();
    private readonly ObjectPool<IConnection> _connectionPool;
    public ProducerRequestService( IEnumerable<ITraceWriter> traceWriterList, ObjectPool<IConnection> connectionPool)
    {
        _connectionPool = connectionPool;
        _traceWriterList = traceWriterList;
        
        var connection = _connectionPool.Get();
        var channel = connection.CreateModel();
        
        replyQueueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };
        
        channel.BasicConsume(consumer: consumer,
            queue: replyQueueName,
            autoAck: true);
        
        _connectionPool.Return(connection);
    }

    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse, TRequest>(HttpRequestData requestData,
        HttpConnectionData connectionData = default) where TRequest : class
        where TResponse : class 
    {
        var connection = _connectionPool.Get();
        using var channel = connection.CreateModel();
        var props = channel.CreateBasicProperties();
        var correlationId = new Uuid7().ToString(); 
        
        props.CorrelationId = correlationId; 
        props.ReplyTo = replyQueueName;
        
        foreach (var traceWriterList in _traceWriterList)
        {
            props.Headers.Add(traceWriterList.Name,traceWriterList.GetValue());
        }


        try
        {
            var res = await SendMessegeAsync<TResponse>(requestData, connectionData, correlationId, channel, props);
            return new HttpResponse<TResponse>()
            {
                Body = res,
                StatusCode = HttpStatusCode.OK
            };
        }
           
        finally
        {
            _connectionPool.Return(connection);
        }
    }

    private async Task<TResponse> SendMessegeAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData,
        string correlationId, IModel channel, IBasicProperties props)
    {
        var json = JsonConvert.SerializeObject(requestData.Body);
        var messegeBytes = Encoding.UTF8.GetBytes(json);
       
       
        var tcs = new TaskCompletionSource<string>();
        _callbackMapper.TryAdd(correlationId, tcs);
       
        channel.BasicPublish(exchange: String.Empty,
            routingKey: "rpc/"+requestData.Uri,
            basicProperties: props,
            body: messegeBytes);

        connectionData.CancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));

        var ans = await tcs.Task;
        var res = JsonConvert.DeserializeObject<TResponse>(ans);
        return res;
    }
}