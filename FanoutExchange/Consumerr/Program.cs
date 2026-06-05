using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection=await factory.CreateConnectionAsync();
using var channel=await connection.CreateChannelAsync();

Console.WriteLine("waiting for message");

await  ConsumeMessage("q.fanout1");
await  ConsumeMessage("q.fanout2");
await  ConsumeMessage("q.fanout3");
await  ConsumeMessage("q.fanout4");
await  ConsumeMessage("q.fanout5");

Console.ReadLine();

async Task ConsumeMessage(string queue)
{
    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync +=async (sender, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        //Write ur method here , to process the message

        Console.WriteLine($" [x] Received from queue [{queue}]: {message}");
        await Task.CompletedTask;

    };
    await channel.BasicConsumeAsync(queue: queue,
                            autoAck: true,
                            consumer: consumer);

}