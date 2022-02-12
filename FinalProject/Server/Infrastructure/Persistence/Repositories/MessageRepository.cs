using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class MessageRepository : Repository<Message, ApplicationDbContext>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
