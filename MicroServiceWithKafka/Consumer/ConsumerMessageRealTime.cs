using MassTransit;
using MediatR;
using MicroServiceWithKafka.MessageDto;
using MicroServiceWithKafka.ServiceCommand;

namespace MicroServiceWithKafka.Consumer
{
    public class ConsumerMessageRealTime : IConsumer<KafkaMessageReceivePerson>
    {
        private readonly IMediator mediator;

        public ConsumerMessageRealTime(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<KafkaMessageReceivePerson> context)
        {
            Console.WriteLine($"Consummer message: {context.Message.Value}");
            var teste = await mediator.Send(new KafkaMessageCommand(context.Message));
        }
    }
}
