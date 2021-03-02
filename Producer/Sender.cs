using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            // Pre-requisites: Install Erlang 23.0 and RabbitMQ 3.8.8
            // Install RabbitMQ plugins:
            // C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.8\sbin> rabbitmq-plugins enable rabbitmq_management
            // RabbitMQ management: http://localhost:15672
            // Credentials: guest/guest

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest", false, false, false, null);

                    string message = "Getting started with .Net Core RabbitMQ";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "BasicTest", null, body);
                    Console.WriteLine($"Sent message: {message}");
                }

                Console.WriteLine("Press [enter] to exit the Sender App...");
                Console.ReadKey();
            }
        }
    }
}
