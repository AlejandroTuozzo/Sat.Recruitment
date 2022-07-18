using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Domain;
using System.Collections.Generic;
using System;

namespace Sat.Recruitment.Api.Business
{
    public class MoneyGiftHandler
    {
        protected decimal GetGift(decimal money, decimal percentage)
        {
            return money * (1 + percentage);
        }
    }
}
