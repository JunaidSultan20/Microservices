﻿using Microsoft.Extensions.Options;

namespace AdventureWorks.Messaging.Services;

public class MessageProducer(IOptionsMonitor<RabbitMqOptions> options) : IMessageProducer
{
    private readonly RabbitMqOptions _options = options.CurrentValue;

    public void SendMessage<T>(string queue,
                               string exchangeName,
                               string exchangeType,
                               string routeKey,
                               T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateChannel();

        channel.ExchangeDeclare(exchangeName, exchangeType);

        channel.QueueDeclare(queue: queue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.QueueBind(queue: queue, exchange: exchangeName, routeKey);

        var json = JsonConvert.SerializeObject(message);

        var body = Encoding.UTF8.GetBytes(json);

        //var properties = channel.CreateBasicProperties();

        //properties.Persistent = true;

        channel.BasicPublish(exchange: exchangeName,
                             routingKey: routeKey,
                             body: body);
    }
}