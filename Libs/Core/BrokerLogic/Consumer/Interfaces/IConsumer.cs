using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleCore.BrokerLogic.Consumer.Interfaces;

public interface IConsumer<T>
{
    Task Consume(object sender, BasicDeliverEventArgs @event, T messege, IModel chanel );
}