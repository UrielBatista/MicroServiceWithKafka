using MassTransit;
using MicroServiceWithKafka.MessageDto;
using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.Producer
{
    public class KafkaMessageProducer : IKafkaMessageProducer
    {
        private readonly ITopicProducer<KafkaMessage> topicProducer;
        private readonly ITopicProducer<KafkaMessageReceivePerson> topicProducerPerson;
        private readonly ITopicProducer<BuildPerson> topicProducerToBuildPerson;

        public KafkaMessageProducer(
            ITopicProducer<KafkaMessage> topicProducer, 
            ITopicProducer<KafkaMessageReceivePerson> topicProducerPerson, 
            ITopicProducer<BuildPerson> topicProducerToBuildPerson)
        {
            this.topicProducer = topicProducer;
            this.topicProducerPerson = topicProducerPerson;
            this.topicProducerToBuildPerson = topicProducerToBuildPerson;
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

        public async Task ProducerToPersonBuild(string message)
        {
            await topicProducerToBuildPerson.Produce(new BuildPerson
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });
        }
    }
}
