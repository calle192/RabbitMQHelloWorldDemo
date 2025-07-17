using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
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

        string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "hello",
            body: body
            );

        Console.WriteLine($" [x] Sent '{message}'");
    }
}
