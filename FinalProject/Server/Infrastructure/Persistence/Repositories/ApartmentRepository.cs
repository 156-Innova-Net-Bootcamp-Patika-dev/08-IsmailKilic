using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ApartmentRepository : Repository<Apartment, ApplicationDbContext>, IAparmentRepository
    {
        public ApartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
