namespace MicroServiceWithKafka.Producer
{
    public interface IKafkaMessageProducer
    {
        Task ProducerMessage(string message);
    }
}
