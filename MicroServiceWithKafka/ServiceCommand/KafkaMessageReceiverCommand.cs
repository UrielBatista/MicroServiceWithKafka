using MediatR;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageReceiverCommand : IRequest<string>
    {
        public KafkaMessageReceiverCommand(KafkaMessageReceivePython kafkaMessageReceivePython)
        {
            this.kafkaMessageReceivePython = kafkaMessageReceivePython;
        }

        public KafkaMessageReceivePython kafkaMessageReceivePython { get; set; }
    }
}