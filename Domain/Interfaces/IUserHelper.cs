using Sat.Recruitment.Api.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Api.Business.Interfaces
{
    public interface IUserHelper
    {
        decimal? CalculateUserGift(UserType? userType, decimal? money);

        string NormalizeMail(string email);
    }
}
