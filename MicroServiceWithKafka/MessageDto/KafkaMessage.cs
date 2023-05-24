namespace MicroServiceWithKafka.MessageDto
{
    public class KafkaMessage
    {
        public string Key { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
