using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection=await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

Console.WriteLine(" [*] Waiting for messages...");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // write ur method here to process the message

    Console.WriteLine($" [x] Received {message}");
};
await channel.BasicConsumeAsync(
    queue: "q.tpc3",
    autoAck: true,
    consumer: consumer);

Console.WriteLine(" Press [enter] to exit");
Console.ReadLine();