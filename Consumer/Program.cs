using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Program
{
    static async Task Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
            Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync("hello", durable: false, exclusive: false, autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x]Received '{message}'");

            //simulate some async work
            await Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: "hello", autoAck: true, consumer: consumer);

        Console.WriteLine(" [*] Waiting for messages. To exit press CTRL + C");
        await Task.Delay(Timeout.Infinite);
    }
}