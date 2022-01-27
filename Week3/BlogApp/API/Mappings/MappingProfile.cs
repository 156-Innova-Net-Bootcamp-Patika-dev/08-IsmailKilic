using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        // Mapping implementations
        public MappingProfile()
        {
            // User -> LoginResponse
            CreateMap<User, LoginResponse>();

            // RegisterDto -> User
            CreateMap<RegisterDto, User>();

            // CategoryDto -> Category
            CreateMap<CreateCategoryDto, Category>();

            // PostDto -> Post
            CreateMap<CreatePostDto, Post>();
            // PostDto -> Post
            CreateMap<CreateCommentDto, Comment>();
        }
    }
}
