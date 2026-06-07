using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection =await factory.CreateConnectionAsync();
using var channel=await connection.CreateChannelAsync();

Console.WriteLine("Consumer is running and waiting for messages...");

var consumer=new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message=Encoding.UTF8.GetString(body);

    // write ur method here to process the message
    Console.WriteLine($"Received message: {message}");
};
await channel.BasicConsumeAsync(
                                queue: "q.header2",
                                autoAck: true,
                                consumer: consumer);
Console.ReadLine();