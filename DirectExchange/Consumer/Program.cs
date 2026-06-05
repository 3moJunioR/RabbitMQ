using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

Console.WriteLine($"Waiting fo message");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message=Encoding.UTF8.GetString(body);

    // write ur method which will do something here

    Console.WriteLine($"[X] {message} were received successfully");
};
await channel.BasicConsumeAsync(queue: "q-notification",
                          autoAck: true,
                          consumer: consumer);