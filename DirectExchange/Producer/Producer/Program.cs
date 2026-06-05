using RabbitMQ.Client;
using System.Text;

var factory =new ConnectionFactory() {HostName="localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

const string message = "Vamooooooooooos";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: "amq.direct"
                                , routingKey: "k-notification"
                                , body: body);
Console.WriteLine($"[X] {message} were sent successfully");
Console.ReadLine();