using Sat.Recruitment.Api.Business.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Sat.Recruitment.Api.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace Sat.Recruitment.Api.DataAccess
{
    public class UserRepository: IUserRepository
    {
        private List<User> _users = new List<User>();

        /// <summary>
        /// Always Get all the data of the file to get all the information in case it has been modified
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllAsync()
        {
            using (var reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    string[] line = (await reader.ReadLineAsync()).Split(',');
                    //I consider that although they are not mandatory, the file respects the format
                    if (line == null || line.Length < 6) continue;
                
                    _users.Add(MapStringToUser(line));
                }
            }

            return _users;
        }

        public async Task<List<User>> GetListAsync(Func<User, bool> filters)
        {
            return (await GetAllAsync()).Where(filters).ToList();
        }

        public async Task<bool> InsertAsync(User newUser)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            using (FileStream fileStream = new FileStream(path, FileMode.Append))
            {
                using (var fw = new StreamWriter(fileStream))
                {
                    string userStr = MapUserToString(newUser);
                    await fw.WriteLineAsync(userStr);
                }
            }
            return true;
        }

        /// <summary>
        /// If it doesn't exist, create it
        /// </summary>
        /// <returns></returns>
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            return new StreamReader(fileStream);
        }

        private string MapUserToString(User user)
        {
            return string.Join(",", new string[] { user.Name, user.Email, user.Phone, user.Address,
                                                    user.UserType.ToString(), user.Money.ToString() });
        }

        private User MapStringToUser(string[] userLine)
        {
            return new User
            {
                Name = userLine[0].ToString(),
                Email = userLine[1].ToString(),
                Phone = userLine[2].ToString(),
                Address = userLine[3].ToString(),
                UserType = Enum.Parse<UserType>(userLine[4].ToString()),
                Money = decimal.Parse(userLine[5].ToString())
            };
        }
    }
}