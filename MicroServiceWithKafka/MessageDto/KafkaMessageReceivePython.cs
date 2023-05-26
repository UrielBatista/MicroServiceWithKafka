using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.MessageDto
{
    public class KafkaMessageReceivePython
    {
        public string Key { get; set; } = default!;
        public PersonDtoReturn Value { get; set; } = default!;
    }
}
