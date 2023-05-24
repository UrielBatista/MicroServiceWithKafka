using MediatR;
using MicroServiceWithKafka.MessageDto.Person;
using MicroServiceWithKafka.RefitServices;
using Refit;
using System.Net;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageCommandHandler : IRequestHandler<KafkaMessageCommand, string>
    {
        private readonly IPersonServices _personServices;

        public KafkaMessageCommandHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        async Task<string> IRequestHandler<KafkaMessageCommand, string>.Handle(KafkaMessageCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            PersonDtoReturn? checkExistenceOfPerson;
            
            var id = request.kafkaMessageReceivePerson.Value.Id;
            try
            {
                checkExistenceOfPerson = await _personServices.SearchOnePerson(id, cancellationToken);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Pessoa nao existe. Tente outra pessoa");
                return "tteste";
            }

            var name = checkExistenceOfPerson.Pessoas!.Nome;
            var email = checkExistenceOfPerson.Pessoas!.Email;

            string maskedEmail = MaskEmail(email!);

            Console.WriteLine($"A pessoa com o nome de {name} foi cadastrada\nJuntamento com o email: {maskedEmail}");
            var result = $"A pessoa com o nome de {name} foi cadastrada\nJuntamente com o email: {maskedEmail}";
            return await Task.FromResult(result);
        }

        public static string MaskEmail(string email)
        {
            int index = email.IndexOf('@');

            if (index > 1)
            {
                var maskedPart = new string('*', index - 1);
                email = email[0] + maskedPart + email.Substring(index);
            }

            return email;
        }
    }
}