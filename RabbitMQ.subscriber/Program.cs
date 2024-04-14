using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

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

            //true yaparsak kaç tane subscriber varsa hepsine toplam 5 mesaj gönderir.
            //false yaparsak her bir subscriber'a beşer mesaj gönderir.
            channel.BasicQos(0,5,false);


            var consumer = new EventingBasicConsumer(channel);

            //autoAck false verirsek RabbitMQ subscriber'a mesaj gönderdiğinde bu mesaj doğru veya yanlış işlensede bu kuyruktan siler.
            //autoAck true verirsek gelen mesajı doğru bir şekilde işlerse kuyruktan silinmesi için haber eder.
            channel.BasicConsume("hello-queue", false, consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj:" + message);
                //oluşturulan tagı rabbitmq ya gönderiyor, ulaştıktan sonra kuyruktan siler.
                channel.BasicAck(e.DeliveryTag,false);
            };
            Console.ReadLine();
        }
    }
}
