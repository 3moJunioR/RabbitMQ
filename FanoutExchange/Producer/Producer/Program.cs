using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel= await connection.CreateChannelAsync();

const string message = "8ibna w 8ab elebda3";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange:"amq.fanout",
                                routingKey: string.Empty,
                                body: body);

Console.WriteLine($" [x] Sent { message}");