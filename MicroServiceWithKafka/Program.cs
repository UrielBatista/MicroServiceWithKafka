using MicroServiceWithKafka.Extensions;
using MicroServiceWithKafka.Producer;
using MicroServiceWithKafka.RefitServices;
using MicroServiceWithKafka.ServiceCommand;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var refitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
}));

builder.Services.AddTransient<IKafkaMessageProducer, KafkaMessageProducer>();
builder.Services.AddKafkaConfiguration(builder.Configuration);
builder.Services.AddRefitClient<IPersonServices>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://10.0.0.4:5001"))
    .AddSystemTokenAuthorization();

_ = builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(KafkaMessageCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
