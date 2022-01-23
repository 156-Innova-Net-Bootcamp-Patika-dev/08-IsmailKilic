using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Business.Helpers.Jwt;
using Data.EfCore;
using Entities.Concrete;
using Entities.Dtos;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private EfCoreUserRepository _userRepository;
        private IMapper _mapper;
        private IJwtUtils _jwtUtils;

        public UserService(EfCoreUserRepository userRepository, IMapper mapper, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public LoginResponse Login(LoginDto model)
        {
            var user = _userRepository.GetByEmail(model.Email);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new BadRequestException("Email or password is incorrect");

            // authentication successful
            var response = _mapper.Map<LoginResponse>(user);
            response.Token = _jwtUtils.Generate(user);
            return response;
        }

        public async Task Register(RegisterDto model)
        {
            var user = _userRepository.GetByEmail(model.Email);
            if (user != null)
            {
                throw new BadRequestException("Email '" + model.Email + "' is already taken");
            }

            // map model to new user object
            var newUser = _mapper.Map<User>(model);

            // hash password
            newUser.Password = BCryptNet.HashPassword(model.Password);

            // save user
            await _userRepository.Add(newUser);
        }
    }
}
