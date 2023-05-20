using MediatR;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageCommandHandler : IRequestHandler<KafkaMessageCommand, IEnumerable<string>>
    {
        async Task<IEnumerable<string>> IRequestHandler<KafkaMessageCommand, IEnumerable<string>>.Handle(KafkaMessageCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = request.KafkaMessage.Value!.Split();
            var dataListString = new List<string>();

            foreach(var dataOne in result)
            {
                dataListString.Add(dataOne);
            }
            Console.WriteLine($"Primeiro item da lista: {dataListString.FirstOrDefault()}");
            return await Task.FromResult(dataListString);
        }
    }
}