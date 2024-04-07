using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ApiSample.Applicattion.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedEntranceController : ControllerBase
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly string _queueName;
    private readonly string _exchangeName;
    private readonly string _routingKey;

    public FeedEntranceController(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _queueName = "your_queue_name";
        _exchangeName = "your_exchange_name";
        _routingKey = "your_routing_key";
    }

    [HttpPost("startListening")]
    public async Task<IActionResult> StartListening()
    {
        try
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

                channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Mensagem recebida: {0}", message);
                };

                await Task.Run(() => channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer));

                return Ok("Listening started.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 