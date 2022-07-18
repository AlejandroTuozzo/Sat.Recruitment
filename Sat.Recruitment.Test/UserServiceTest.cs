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
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly Mock<IUserHelper> _userHelper;

        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userValidator = new UserValidator();
            _userHelper = new Mock<IUserHelper>();
        }

        [Fact]
        public async void CreateUser_Success()
        {
            var newUser = new User()
            {
                Name = "Cosme",
                Email = "CosmeFulanito@gmail.com",
                Address = "Calle Falsa 123",
                Phone = "+534645213542",
                UserType = UserType.Normal,
                Money = 124
            };
            
            _userRepository.Setup(x => x.GetListAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(new List<User>());

            _userRepository.Setup(x => x.InsertAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            var _userService = new UserService(_userRepository.Object, _userHelper.Object);
            var created = await _userService.CreateUserAsync(newUser);
            Assert.True(created);
        }

        [Fact]
        public void CreateUser_Duplicated()
        {
            var newUser = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Garay y Otra Calle",
                Phone = "+534645213542",
                UserType = UserType.SuperUser,
                Money = 124
            };

            _userRepository.Setup(x => x.GetListAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(new List<User>() { newUser });

            var _userService = new UserService(_userRepository.Object, _userHelper.Object);
            var userController = new UsersController(_userService, _userValidator);

            Task<Exception> result =
                Assert.ThrowsAsync<Exception>(async () =>
                {
                    await _userService.CreateUserAsync(newUser);
                });

            Assert.Equal("The user is duplicated", result.Result.Message);
        }
    }
}
