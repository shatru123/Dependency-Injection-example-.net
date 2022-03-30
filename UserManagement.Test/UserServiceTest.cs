using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UserManagement.Services;
using UserManagement.DbRepo;
using UserManagement.Shared.Models;

namespace UserManagement.Test
{
    public class UserServiceTest
    {
        private readonly UserService _userService;       
        private readonly Mock<IDbContext> _dbContextMock = new Mock<IDbContext>();
        public UserServiceTest()
        {
            _userService = new UserService(_dbContextMock.Object);
        }

        [Test] 
        public async Task GetSingleUser_Test()
        {
            //Arrange
            var userId = System.Guid.NewGuid().ToString();
            var userData = new User()
            {
                Id =  userId,
                Address = "Pune",
                ContactNo = "1234567890",
                Email = "ambhoreshatrughna@gmail.com",
                Name = "Shatrughna Shivaji Ambhore"
            };
            _dbContextMock.Setup(u => u.GetSingleUser(userId)).ReturnsAsync(userData);
            //Act
            var user = await _userService.GetSingleUser(userId);
            //Assert 
            _dbContextMock.Verify(m => m.GetSingleUser(userId), Times.Once);
            Assert.AreEqual(userId, user.Id);
             
            
        }
        
        [Test] 
        public async Task GetAllUser_Test()
        {
            //Arrange
            var userList = new List<User>()
            {
                new User()
                {
                    Id = System.Guid.NewGuid().ToString(),
                    Address = "Pune",
                    ContactNo = "1234567890",
                    Email = "ambhoreshatrughna@gmail.com",
                    Name = "Shatrughna Shivaji Ambhore"
                },
                new User()
                {
                    Id = System.Guid.NewGuid().ToString(),
                    Address = "Mumbai",
                    ContactNo = "11111110",
                    Email = "user@gmail.com",
                    Name = "User Name"
                }
            };

            _dbContextMock.Setup(u => u.GetAllUsers()).ReturnsAsync(userList);
            //Act
            var users = await _userService.GetAllUsers();
            //Assert 
            Assert.IsNotNull(users);
            _dbContextMock.Verify(m => m.GetAllUsers(), Times.Once);
        }
        
        [Test] 
        public async Task AddUser_Test()
        {
            //Arrange
            var user = new User()
            {
                Id = System.Guid.NewGuid().ToString(),
                Address = "Pune",
                ContactNo = "1234567890",
                Email = "ambhoreshatrughna@gmail.com",
                Name = "Shatrughna Shivaji Ambhore"
            };

            _dbContextMock.Setup(u => u.AddNewUser(user)).ReturnsAsync(true);
            //Act
            var users = await _userService.AddNewUser(user);
            //Assert 
            Assert.IsTrue(users);
            _dbContextMock.Verify(m => m.AddNewUser(user), Times.Once);
        }
        public async Task UpdateUser_Test()
        {
            //Arrange
            var user = new User()
            {                
                Address = "Pune MH",
                ContactNo = "1234567890",
                Email = "useremail@gmail.com",
                Name = "Shatrughna Ambhore"
            };

            _dbContextMock.Setup(u => u.UpdateUser(user)).ReturnsAsync(true);
            //Act
            var users = await _userService.UpdateUser(user);
            //Assert 
            Assert.IsTrue(users);
            _dbContextMock.Verify(m => m.UpdateUser(user), Times.Once);
        }
        
        [Test] 
        public async Task DeleteUser_Test()
        {
            //Arrange
            var userId = System.Guid.NewGuid().ToString();

            _dbContextMock.Setup(u => u.DeleteUser(userId)).ReturnsAsync(true);
            //Act
            var result = await _userService.DeleteUser(userId);
            //Assert 
            Assert.IsTrue(result);
            _dbContextMock.Verify(m => m.DeleteUser(userId), Times.Once);

        }
    }
}