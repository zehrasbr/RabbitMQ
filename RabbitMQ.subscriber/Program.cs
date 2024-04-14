using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://jawtezjl:BTcaEc4oW-3CfzisydEXZfQutSSsTck3@fish.rmq.cloudamqp.com/jawtezjl");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //kuyruk burada oluşturmak zorunda değiliz.
            //eğer oluşturacaksak publisher kısmı ile aynı olmalı kuyruk.
            channel.QueueDeclare("hello-queue", true, false, false);

            
            Console.ReadLine();
        }
    }
}
