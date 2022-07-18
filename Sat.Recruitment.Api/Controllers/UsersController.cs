using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.DTO;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<User> _userValidator;
        public UsersController(IUserService userService, IValidator<User> userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            User newUser = MapUser(name, email, address, phone, userType, money);

            var validationResult = await _userValidator.ValidateAsync(newUser);

            if (!validationResult.IsValid)
            {
                return GetResult(false, string.Join(". ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            try
            {
                await _userService.CreateUserAsync(newUser);

                Debug.WriteLine("User Created");
                return GetResult(true, "User Created");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return GetResult(false, e.Message);
            }
        }

        // Map parameters to User
        private static User MapUser(string name, string email, string address, string phone, string userType, string money)
        {
            return new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = Enum.TryParse(userType, out UserType parsedUserType) ? parsedUserType : (UserType?)null,
                Money = decimal.TryParse(money, out decimal parsedMoney) ? parsedMoney : (decimal?)null
            };
        }

        private Result GetResult(bool success, string message)
        {
            return new Result()
            {
                IsSuccess = success,
                Message = message
            };
        }
    }
}
