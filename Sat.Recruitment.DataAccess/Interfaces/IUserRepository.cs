using Sat.Recruitment.Api.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetListAsync(Func<User, bool> filters);
        Task<bool> InsertAsync(User entity);
    }
}
