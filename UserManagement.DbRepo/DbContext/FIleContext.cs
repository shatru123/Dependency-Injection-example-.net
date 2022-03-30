
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Shared.Models;

namespace UserManagement.DbRepo
{
    public class FileContext : IDbContext
    {
        string filePath = string.Empty;
        private readonly ILogger<FileContext> _logger;
        public FileContext(ILogger<FileContext> logger)
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "SampleData.json");
            _logger = logger;
        }
        public async Task<bool> AddNewUser(User userData)
        {
            var userDetails = await GetExistingUserData();
            if (userDetails.Users.Any())
            {
                userDetails.Users.Add(userData);
            }
            else
            {
                userDetails = new UserData()
                {
                    Users = new List<User>()
                    {
                        userData
                    }
                };
            }
            return await SaveDataAsync(userDetails);
        }
        public async Task<UserData> GetExistingUserData()
        {
            var userData = await GetUserDetails();
            if (userData.Users.Any())
                return userData;
            return null;
        }

        private async Task<UserData> GetUserDetails()
        {
            try
            {
                var data = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<UserData>(data);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        private async Task<bool> SaveDataAsync(UserData userDetails)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(userDetails, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, jsonString);
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var userDetails = await GetExistingUserData();
            if (userDetails.Users is not null)
            {
                var noOfUsersDeleted = userDetails.Users.RemoveAll(u => u.Id == userId);
                return noOfUsersDeleted > 0 ? await SaveDataAsync(userDetails) : false;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userData = await GetUserDetails();
            return userData.Users;
        }

        public async Task<User> GetSingleUser(string userId)
        {
            var userDetails = await GetExistingUserData();
            if (userDetails.Users.Where(u => u.Id == userId).Any())
                return userDetails.Users.Where(u => u.Id == userId).FirstOrDefault();
            return null;
        }

        public async Task<bool> UpdateUser(User userData)
        {
            var userDetails = await GetExistingUserData();
            if (userDetails.Users.Any())
            {
                var index = userDetails.Users.FindIndex(user => user.Id == userData.Id);
                if (index != -1)
                    userDetails.Users[index] = userData;
                return await SaveDataAsync(userDetails);
            }
            return false;
        }
    }
}