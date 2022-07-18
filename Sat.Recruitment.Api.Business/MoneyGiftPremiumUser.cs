using Sat.Recruitment.Api.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Api.Business
{
    public class MoneyGiftPremiumUser : MoneyGiftHandler, IMoneyGiftHandler
    {
        public decimal GetGiftByUserType(decimal money)
        {
            return GetGift(money, Convert.ToDecimal(2));
        }
    }
}
