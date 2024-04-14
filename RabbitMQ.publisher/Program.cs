﻿using RabbitMQ.Client;
using System;

namespace RabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://jawtezjl:BTcaEc4oW-3CfzisydEXZfQutSSsTck3@fish.rmq.cloudamqp.com/jawtezjl");

            using var connection = factory.CreateConnection();

            //bu kanal üzerinden rabbitmq ile haberleşebiliriz.
            var channel = connection.CreateModel();

            //true yaparsak rabbitmq'ya restart atsakta kuyruklar kaybolmaz.
            //subscriber'a bağlanmak için(farklı kanallardan bağlanmak için) false yaptık.
            //Eğer sadece bulunduğu kanala bağlanmak istesek true olmalı.  
            //autoDelete otomatik silinmesini istemediğimiz için false.
            channel.QueueDeclare("hello-queue", true, false,false);

            string message = "hello world";
        }
    }
}