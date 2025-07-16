using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher<String> _passwordHasher;

        public UserRepository(AppDbContext appDbContext) {
            _dbContext = appDbContext;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.Password = _passwordHasher.HashPassword(user.Email, user.Password);
            var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.Ordinal));
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveUser(Guid id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
