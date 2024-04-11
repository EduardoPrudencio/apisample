using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ApiSample.Applicattion.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedEntranceController : ControllerBase
{
    private readonly ConnectionFactory _connectionFactory;
   // IConnection connection;
    private readonly string _queueName;
    private readonly string _exchangeName;
    private readonly string _routingKey;

    public FeedEntranceController()
    {
        _queueName = "omnycontent";
        _exchangeName = "fanoutFeed";
        _routingKey = "";

        _connectionFactory = new ConnectionFactory();
        _connectionFactory.HostName = "localhost";
        _connectionFactory.UserName = "guest";
        _connectionFactory.Password = "guest";
       // _connectionFactory.CreateConnection();
    }

    [HttpPost("startListening")]
    public async Task<IActionResult> StartListening()
    {
        try
        {
            EventingBasicConsumer consumer;
            
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                    arguments: null);

                channel.QueueDeclare(queue: _queueName,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

               consumer = new EventingBasicConsumer(channel);

               consumer.Received += (model, ea) =>
               {
                  var body = ea.Body.ToArray();
                  var message = Encoding.UTF8.GetString(body);
                  Console.WriteLine(" [x] Received {0}", message);
                  channel.BasicNack(ea.DeliveryTag, false, false);
               };

               channel.BasicQos(0, 1, false);

               channel.BasicConsume(queue: _queueName,
                                    autoAck: false,
                                    consumer: consumer);
           

                return Ok("Listening started.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 