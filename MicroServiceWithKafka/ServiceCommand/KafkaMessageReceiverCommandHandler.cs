using MediatR;
using MicroServiceWithKafka.MessageDto.Person;
using MicroServiceWithKafka.RefitServices;
using Refit;
using System.Net;

namespace MicroServiceWithKafka.ServiceCommand
{
    public class KafkaMessageReceiverCommandHandler : IRequestHandler<KafkaMessageReceiverCommand, PersonDtoReturn>
    {
        private readonly IPersonServices _personServices;

        public KafkaMessageReceiverCommandHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        async Task<PersonDtoReturn> IRequestHandler<KafkaMessageReceiverCommand, PersonDtoReturn>.Handle(KafkaMessageReceiverCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var createPeoplePayload = request.kafkaMessageReceivePython.Value;

            var person = await RequestPersonMethod(createPeoplePayload, cancellationToken).ConfigureAwait(false);
            if (person.Pessoas is null) 
            {
                return await CreatePersonMethod(createPeoplePayload, cancellationToken).ConfigureAwait(false);
            }
            return person;

        }

        private async Task<PersonDtoReturn> CreatePersonMethod(PersonDtoReturn personDtoReturn, CancellationToken cancellationToken)
        {
            PersonDtoReturn? personDto;
            try
            {
                personDto = await _personServices
                    .CreatePerson(
                        personDtoReturn,
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Error in create person with person_id: {personDtoReturn.Pessoas!.Id_Pessoas}");
                return new PersonDtoReturn();
            }

            return personDto;
        }

        private async Task<PersonDtoReturn> RequestPersonMethod(PersonDtoReturn personDtoReturn, CancellationToken cancellationToken)
        {
            PersonDtoReturn? personDto;
            try
            {
                personDto = await _personServices
                    .SearchOnePerson(
                        personDtoReturn.Pessoas!.Id_Pessoas!,
                        cancellationToken)
                    .ConfigureAwait(false);

            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Person with id_pessoas: {personDtoReturn.Pessoas!.Id_Pessoas} already exist in database!");
                return new PersonDtoReturn();
            }

            return personDto;
        }
    }
}