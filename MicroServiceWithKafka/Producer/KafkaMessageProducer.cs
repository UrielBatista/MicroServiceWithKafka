using MassTransit;
using MicroServiceWithKafka.MessageDto;
using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.Producer
{
    public class KafkaMessageProducer : IKafkaMessageProducer
    {
        private readonly ITopicProducer<KafkaMessage> topicProducer;
        private readonly ITopicProducer<KafkaMessageReceivePerson> topicProducerPerson;

        public KafkaMessageProducer(
            ITopicProducer<KafkaMessage> topicProducer, 
            ITopicProducer<KafkaMessageReceivePerson> topicProducerPerson)
        {
            this.topicProducer = topicProducer;
            this.topicProducerPerson = topicProducerPerson;
        }

        public async Task ProducerMessage(string message)
        {
            await topicProducer.Produce(new KafkaMessage
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });
        }

        public async Task ProducerPersonMessager(PersonDtoSender message)
        {
            await topicProducerPerson.Produce(new KafkaMessageReceivePerson
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });
        }
    }
}
