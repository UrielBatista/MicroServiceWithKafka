using MassTransit;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.Consumer
{
    public class ConsumerKafkaMessageRealTime : IConsumer<KafkaMessageReceivePython>
    {
        public Task Consume(ConsumeContext<KafkaMessageReceivePython> context)
        {
            Console.WriteLine($"Consummer message: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }
}
