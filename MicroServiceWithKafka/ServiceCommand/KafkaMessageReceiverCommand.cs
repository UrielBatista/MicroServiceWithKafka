using MediatR;
using MicroServiceWithKafka.MessageDto;
using MicroServiceWithKafka.MessageDto.Person;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageReceiverCommand : IRequest<PersonDtoReturn>
    {
        public KafkaMessageReceiverCommand(KafkaMessageReceivePython kafkaMessageReceivePython)
        {
            this.kafkaMessageReceivePython = kafkaMessageReceivePython;
        }

        public KafkaMessageReceivePython kafkaMessageReceivePython { get; set; }
    }
}