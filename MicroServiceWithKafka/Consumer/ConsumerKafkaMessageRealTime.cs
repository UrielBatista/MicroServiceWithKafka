using MassTransit;
using MediatR;
using MicroServiceWithKafka.MessageDto;
using MicroServiceWithKafka.ServiceCommand;

namespace MicroServiceWithKafka.Consumer
{
    public class ConsumerKafkaMessageRealTime : IConsumer<KafkaMessageReceivePython>
    {
        private readonly IMediator mediator;

        public ConsumerKafkaMessageRealTime(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<KafkaMessageReceivePython> context)
        {
            Console.WriteLine($"Consummer message: {context.Message.Value}");
            _ = await mediator.Send(new KafkaMessageReceiverCommand(context.Message));
        }
    }
}
