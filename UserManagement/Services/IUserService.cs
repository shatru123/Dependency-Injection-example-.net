using System.Threading.Tasks;
using System.Collections.Generic;
using UserManagement.Shared.Models;
namespace UserManagement.Services
{
    public interface IUserService
    {
        public Task<User> GetSingleUser(string userId);
        public Task<List<User>> GetAllUsers();
        public Task<bool> AddNewUser(User userData);
        public Task<bool> UpdateUser(User userData);
        public Task<bool> DeleteUser(string userId);
    }
}