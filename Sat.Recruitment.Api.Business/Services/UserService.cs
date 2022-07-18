using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserHelper _userHelper;

        public UserService(IUserRepository userRepository, IUserHelper userHelper)
        {
            _userRepository = userRepository;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Validate duplicity and insert
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> CreateUserAsync(User newUser)
        {
            // Get existing user
            var existingUsers = await _userRepository
                .GetListAsync(u => (u.Email == newUser.Email || u.Phone == newUser.Phone)
                    || (u.Name == newUser.Name && u.Address == newUser.Address));

            if (existingUsers.Any())
            {
                throw new Exception("The user is duplicated");
            }
            
            newUser.Email = _userHelper.NormalizeMail(newUser.Email);
            newUser.Money = _userHelper.CalculateUserGift(newUser.UserType, newUser.Money);

            return await _userRepository.InsertAsync(newUser);
        }
    }
}
