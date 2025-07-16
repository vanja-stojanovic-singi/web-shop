using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task UpdateUser(User user);
        Task RemoveUser(Guid id);
    }
}
