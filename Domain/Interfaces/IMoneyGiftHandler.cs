using Sat.Recruitment.Api.Domain;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Business.Interfaces
{
    public interface IMoneyGiftHandler
    {
        decimal GetGiftByUserType(decimal money);
    }
}
