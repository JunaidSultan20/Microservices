using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Constants = AdventureWorks.Common.Constants.Constants;

namespace AdventureWorks.Sales.Api.BackgroundServices;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqBackgroundService(IOptions<RabbitMqOptions> options)
    {
        var rabbitMqConfig = options.Value;
        var factory = new ConnectionFactory
        {
            HostName = rabbitMqConfig.Hostname,
            Port = rabbitMqConfig.Port,
            UserName = rabbitMqConfig.Username,
            Password = rabbitMqConfig.Password
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateChannel();
        InitRabbitMq();
    }

    private void InitRabbitMq()
    {
        _channel.ExchangeDeclare("SalesExchange", "direct");

        _channel.QueueDeclare(queue: Constants.SalesQueue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        _channel.QueueBind(queue: Constants.SalesQueue, exchange: "SalesExchange", "sales_route");
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            // Received message
            var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

            // Acknowledge the received message
            _channel.BasicAck(ea.DeliveryTag, false);

            // Deserialized Message
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var json = JsonConvert.DeserializeObject<object>(message);
            Console.WriteLine("Message From Queue");
            Console.WriteLine(json);
        };

        consumer.Shutdown += OnConsumerShutdown;
        consumer.Registered += OnConsumerRegistered;
        consumer.Unregistered += OnConsumerUnregistered;
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

        _channel.BasicConsume(Constants.SalesQueue, false, consumer);
        return Task.CompletedTask;
    }

    private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
    private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
    private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}