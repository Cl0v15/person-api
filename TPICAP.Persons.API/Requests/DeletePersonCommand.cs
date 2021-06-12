using MediatR;

namespace TPICAP.Persons.API.Requests
{
    public class DeletePersonCommand : IRequest
    {
        public int PersonId { get; }

        public DeletePersonCommand(int personId)
        {
            PersonId = personId;
        }
    }
}
