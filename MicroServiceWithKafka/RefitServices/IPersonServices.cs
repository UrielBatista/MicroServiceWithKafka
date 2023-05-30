using MicroServiceWithKafka.MessageDto.Person;
using Refit;

namespace MicroServiceWithKafka.RefitServices
{
    public interface IPersonServices
    {
        [Get("/api/v1/Persons/search-person?id_pessoa={id_pessoa}")]
        Task<PersonDtoReturn> SearchOnePerson(
            [Query] string id_pessoa, 
            CancellationToken cancellationToken);

        [Post("/api/v1/Persons")]
        Task<PersonDtoReturn> CreatePerson(
            [Body] PersonDtoReturn person,
            CancellationToken cancellationToken);
    }
}
