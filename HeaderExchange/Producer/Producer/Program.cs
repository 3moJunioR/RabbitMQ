using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {HostName="localhost" };
using var connection =await factory.CreateConnectionAsync();
using var channel =await connection.CreateChannelAsync();

var properties = new BasicProperties();
properties.Persistent = true;

Dictionary<string, object?> dictionaryheaders = new Dictionary<string, object?>();
dictionaryheaders.Add("name1", "info");

properties.Headers=dictionaryheaders;

const string message = "Vamoooooooooos";
var body=Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
                                exchange: "amq.headers",
                                routingKey: string.Empty,
                                mandatory: false,
                                basicProperties: properties,
                                body: body);
Console.WriteLine($"message [{message}] were sent successfully");
Console.ReadLine();