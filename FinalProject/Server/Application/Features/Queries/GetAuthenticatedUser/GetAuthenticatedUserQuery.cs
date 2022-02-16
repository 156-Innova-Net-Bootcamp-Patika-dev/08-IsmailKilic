using MediatR;

namespace Application.Features.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQuery : IRequest<GetAuthenticatedUserResponse>
    {
        public string Id { get; set; }
    }
}
