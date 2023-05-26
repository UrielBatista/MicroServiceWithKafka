using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.Producer
{
    public interface IKafkaMessageProducer
    {
        Task ProducerMessage(string message);
        Task ProducerPersonMessager(PersonDtoSender message);
        Task ProducerToPersonBuild(string message);
    }
}
