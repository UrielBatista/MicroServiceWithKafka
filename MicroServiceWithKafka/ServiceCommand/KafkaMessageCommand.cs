using MediatR;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageCommand : IRequest<string>
    {
        public KafkaMessageCommand(KafkaMessageReceivePerson kafkaMessageReceivePerson)
        {
            this.kafkaMessageReceivePerson = kafkaMessageReceivePerson;
        }

        public KafkaMessageReceivePerson kafkaMessageReceivePerson { get; set; }
    }
}