
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.DbRepo;
using UserManagement.Shared.Models;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContext _dbContext;
        public UserService(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public async Task<bool> AddNewUser(User userData)
        {
            return await _dbContext.AddNewUser(userData);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            return await _dbContext.DeleteUser(userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.GetAllUsers(); 
        }

        public async Task<User> GetSingleUser(string userId)
        {
            return await _dbContext.GetSingleUser(userId);
        }

        public async Task<bool> UpdateUser(User userData)
        {
            return await _dbContext.UpdateUser(userData);
        }
    }

}