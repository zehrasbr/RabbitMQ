using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
            //channel.QueueDeclare("hello-queue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);

            //autoAck false verirsek RabbitMQ subscriber'a mesaj gönderdiğinde bu mesaj doğru veya yanlış işlensede bu kuyruktan siler.
            //autoAck true verirsek gelen mesajı doğru bir şekilde işlerse kuyruktan silinmesi için haber eder.
            channel.BasicConsume("hello-queue", true, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:" + message);
            };
                
            Console.ReadLine();
        }
    }
}
