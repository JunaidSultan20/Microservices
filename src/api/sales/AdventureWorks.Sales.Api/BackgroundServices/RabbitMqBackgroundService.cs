using AdventureWorks.Common.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Constants = AdventureWorks.Common.Constants.Constants;

namespace AdventureWorks.Sales.Api.BackgroundServices;

public class RabbitMqBackgroundService(IOptions<RabbitMqOptions> options,
                                        ILogger<RabbitMqBackgroundService> logger) : BackgroundService
{
    private IConnection? _connection;
    private IChannel? _channel;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        RabbitMqOptions config = options.Value;
        var factory = new ConnectionFactory
        {
            HostName = config.Hostname,
            Port = config.Port,
            UserName = config.Username,
            Password = config.Password
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.ExchangeDeclareAsync("SalesExchange", ExchangeType.Direct, cancellationToken: cancellationToken);
        await _channel.QueueDeclareAsync(queue: Constants.SalesQueue,
                                          durable: true,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null,
                                          cancellationToken: cancellationToken);
        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: cancellationToken);
        await _channel.QueueBindAsync(queue: Constants.SalesQueue,
                                       exchange: "SalesExchange",
                                       routingKey: "sales_route",
                                       cancellationToken: cancellationToken);

        _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel!);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            try
            {
                string message = Encoding.UTF8.GetString(ea.Body.ToArray());
                object? json = JsonConvert.DeserializeObject<object>(message);

                logger.LogInformation("Message from queue: {Message}", json);

                // process the message here

                await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process message, requeueing");
                await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
            }
        };

        await _channel!.BasicConsumeAsync(Constants.SalesQueue, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

        // Keep running until cancellation, since BasicConsumeAsync doesn't block
        await Task.Delay(Timeout.Infinite, stoppingToken).ContinueWith(_ => { }, TaskScheduler.Default);
    }

    private Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        logger.LogWarning("RabbitMQ connection shut down: {Reason}", e.ReplyText);
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null) await _channel.CloseAsync(cancellationToken);
        if (_connection is not null) await _connection.CloseAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}