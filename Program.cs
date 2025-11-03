using MQTTnet;
// Server APIs are not available in the referenced MQTTnet package version.
using MQTTnet.Server;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- MQTT Broker Setup ---
// NOTE: MQTT broker startup is disabled because this project references a MQTTnet
// package version that doesn't expose the server API under the MQTTnet.Server
// namespace (see mqttbroker.csproj). If you want an in-process broker, either
// install a version of MQTTnet that includes the server (older versions) or add
// the appropriate server package. The code below is preserved but disabled
// so the Web API can compile and run.
#if false
var mqttFactory = new MqttFactory();
var mqttServerOptions = new MqttServerOptionsBuilder()
    .WithDefaultEndpoint()
    .WithDefaultEndpointPort(1883)
    .Build();

var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);

await mqttServer.StartAsync();
Console.WriteLine("✅ MQTT Broker is running on port 1883");

// Gracefully stop broker when app stops
app.Lifetime.ApplicationStopping.Register(async () =>
{
    await mqttServer.StopAsync();
    Console.WriteLine("🛑 MQTT Broker stopped.");
});
#endif

// --- Start Web API ---
app.Run();
