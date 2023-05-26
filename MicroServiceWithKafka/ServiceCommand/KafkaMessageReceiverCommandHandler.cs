using MediatR;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageReceiverCommandHandler : IRequestHandler<KafkaMessageReceiverCommand, string>
    {
        public Task<string> Handle(KafkaMessageReceiverCommand request, CancellationToken cancellationToken)
        {
            // Receive of python producer topic
            throw new NotImplementedException();
        }
    }
}