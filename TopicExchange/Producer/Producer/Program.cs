using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {HostName="localhost" };
using  var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

const string message = "Vamooooooooooos";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: "amq.topic",
                                routingKey: "h.z",
                                body: body);

Console.WriteLine($"[X] Sent {message}");
Console.ReadLine();
