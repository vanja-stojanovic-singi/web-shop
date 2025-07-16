using Microsoft.AspNetCore.Identity;
using WebShopApi.Models;
using WebShopApi.Repositories;

namespace WebShopApi.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<string> _passwordHasher;

        public UsersService(IUserRepository userRepository) {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<LoggedUser> GetLoggedUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            return new LoggedUser
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> AddUser(User user)
        {
            var existingUser = await this._userRepository.GetUserByEmail(user.Email);

            if (existingUser != null)
            {
                throw new ArgumentException($"User with email address '{user.Email}' already exist!");
            }

            return await this._userRepository.CreateUser(user);
        }

        public async Task<User> UpdateUser(Guid userId, User newUser)
        {
            var user = await this._userRepository.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            user.Name = newUser.Name;
            user.Address = newUser.Address;
            user.City = newUser.City;
            user.Phone = newUser.Phone;
            user.Zip = newUser.Zip;
            user.Birthday = newUser.Birthday;

            await this._userRepository.UpdateUser(user);

            return user;
        }

        public async Task DeleteUser(Guid userId)
        {
            await this._userRepository.RemoveUser(userId);
        }

        public async Task<bool> Validate(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user.Email, user.Password, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return false;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user.Email, user.Password, oldPassword);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                return false;
            }

            user.Password = _passwordHasher.HashPassword(user.Email, newPassword);
            await this._userRepository.UpdateUser(user);

            return true;
        }
    }
}
