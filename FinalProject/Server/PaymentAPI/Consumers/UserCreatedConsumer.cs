using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using PaymentAPI.Data;
using PaymentAPI.Models;
using System;

namespace PaymentAPI.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IMongoRepository<User> userRepository;

        public UserCreatedConsumer(IMongoRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var createdUser = context.Message;
            var user = new User
            {
                UserId = createdUser.UserId,
                UserName = createdUser.UserName,
                Email = createdUser.Email,
                Balance = 1000,
                CreatedAt = DateTime.Now
            };

            await userRepository.InsertOneAsync(user);
        }
    }
}
