using System;
using System.Text;
using RabbitMQ.Client;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Type 'EXIT!' to exit.");
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    while (true)
                    {
                        Console.Write("Message: ");
                        var message = Console.ReadLine();
                        
                        if (message is null or "EXIT!")
                        {
                            break;
                        }
                        
                        var body = Encoding.UTF8.GetBytes(message);
                
                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);
                    }
                }
            }
        }
    }
}
