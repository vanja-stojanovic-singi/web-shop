using WebShopApi.Models;

namespace WebShopApi.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<LoggedUser> GetLoggedUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(Guid userId, User newUser);
        Task DeleteUser(Guid userId);
        Task<bool> Validate(string email, string password);
        Task<bool> ChangePassword(Guid userId, string oldPassword, string newPassword);
    }
}
