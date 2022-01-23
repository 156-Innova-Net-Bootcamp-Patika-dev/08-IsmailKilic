using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        LoginResponse Login(LoginDto model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task Register(RegisterDto model);
        void Delete(int id);
    }
}
