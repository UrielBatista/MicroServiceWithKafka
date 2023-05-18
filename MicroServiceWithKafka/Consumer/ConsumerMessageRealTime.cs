using MassTransit;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.Consumer
{
    public class ConsumerMessageRealTime : IConsumer<KafkaMessage>
    {
        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            return Task.CompletedTask;
        }
    }
}
