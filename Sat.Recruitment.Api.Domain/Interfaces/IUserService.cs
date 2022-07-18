using Sat.Recruitment.Api.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User newUser);
    }
}
