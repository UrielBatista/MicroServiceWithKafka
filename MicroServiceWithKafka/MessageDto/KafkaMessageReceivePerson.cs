using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.MessageDto
{
    public class KafkaMessageReceivePerson
    {
        public string Key { get; set; } = default!;
        public PersonDtoSender Value { get; set; } = default!;
    }
}
