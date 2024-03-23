using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace ExampleCore.BrokerLogic;

public class ConnectionPooledPolicy: IPooledObjectPolicy<IConnection>
{
    private ConnectionFactory _connectionFactory;

    public ConnectionPooledPolicy(
        string host)
    {
        var factory = new ConnectionFactory 
        {        
            HostName = host,
        };
        _connectionFactory = factory;
    }
    
    public IConnection Create()
    {
        return _connectionFactory.CreateConnection();
    }

    public bool Return(IConnection obj)
    {
        if (obj.IsOpen)
        {
            return true;
        }
        
        obj.Dispose();
        return false;
    }
}