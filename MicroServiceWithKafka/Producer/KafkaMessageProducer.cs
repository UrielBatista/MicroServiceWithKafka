using MassTransit;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.Producer
{
    public class KafkaMessageProducer : IKafkaMessageProducer
    {
        private readonly ITopicProducer<KafkaMessage> topicProducer;

        public KafkaMessageProducer(ITopicProducer<KafkaMessage> topicProducer)
        {
            this.topicProducer = topicProducer;
        }

        public async Task ProducerMessage(string message)
        {
            await topicProducer.Produce(new KafkaMessage
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });
        }
    }
}
