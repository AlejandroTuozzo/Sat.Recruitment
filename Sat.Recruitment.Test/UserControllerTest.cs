using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Domain;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private readonly IValidator<User> _userValidator;

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _userValidator = new UserValidator();
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            var newUser = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };

            _userService.Setup(x => x.CreateUserAsync(newUser))
                .Returns(new Task<bool>(() => true));

            var userController = new UsersController(_userService.Object, _userValidator);
            var result = await userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
        }

        [Fact]
        public async Task CreateUser_EmptyAllData()
        {
            var userController = new UsersController(_userService.Object, _userValidator);
            var result = await userController.CreateUser("", "", "", "", "SuperUser", "124");

            Assert.False(result.IsSuccess);
            Assert.Contains("The Name is required", result.Message);
            Assert.Contains("The Email is required", result.Message);
            Assert.Contains("The Email is invalid", result.Message);
            Assert.Contains("The Address is required", result.Message);
            Assert.Contains("The Phone is required", result.Message);
        }

        [Fact]
        public async Task CreateUser_EmptyName()
        {
            var userController = new UsersController(_userService.Object, _userValidator);
            var result = await userController.CreateUser("", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "SuperUser", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal("The Name is required", result.Message);
        }

        [Fact]
        public async Task CreateUser_EmptyEmail()
        {

            var userController = new UsersController(_userService.Object, _userValidator);

            var result = await userController.CreateUser("Agustina", "", "Av. Juan G", "+349 1122354215", "SuperUser", "124");

            Assert.False(result.IsSuccess);
            Assert.Contains("The Email is required", result.Message);
            Assert.Contains("The Email is invalid", result.Message);
        }

        [Fact]
        public async Task CreateUser_InvalidEmail()
        {

            var userController = new UsersController(_userService.Object, _userValidator);

            var result = await userController.CreateUser("Agustina", "Agustina-gmail.com", "Av. Juan G", "+349 1122354215", "SuperUser", "124");

            Assert.False(result.IsSuccess);
            Assert.Contains("The Email is invalid", result.Message);
        }

        [Fact]
        public async Task CreateUser_EmptyAddress()
        {

            var userController = new UsersController(_userService.Object, _userValidator);

            var result = await userController.CreateUser("Agustina", "Agustina@gmail.com", "", "+349 1122354215", "SuperUser", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal("The Address is required", result.Message);
        }

        [Fact]
        public async Task CreateUser_EmptyPhone()
        {

            var userController = new UsersController(_userService.Object, _userValidator);

            var result = await userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "", "SuperUser", "");

            Assert.False(result.IsSuccess);
            Assert.Equal("The Phone is required", result.Message);
        }
    }
}