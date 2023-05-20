using MicroServiceWithKafka.Extensions;
using MicroServiceWithKafka.Producer;
using MicroServiceWithKafka.ServiceCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IKafkaMessageProducer, KafkaMessageProducer>();
builder.Services.AddKafkaConfiguration(builder.Configuration);

_ = builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(KafkaMessageCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
