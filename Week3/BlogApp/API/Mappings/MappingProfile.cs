using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User -> LoginResponse
            CreateMap<User, LoginResponse>();

            // RegisterDto -> User
            CreateMap<RegisterDto, User>();

            // CategoryDto -> Category
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
