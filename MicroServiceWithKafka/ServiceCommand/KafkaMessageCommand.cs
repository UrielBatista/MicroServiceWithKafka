using MediatR;
using MicroServiceWithKafka.MessageDto;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageCommand : IRequest<IEnumerable<string>>
    {
        public KafkaMessageCommand(KafkaMessage kafkaMessage)
        {
            this.KafkaMessage = kafkaMessage;
        }

        public KafkaMessage KafkaMessage { get; set; }
    }
}