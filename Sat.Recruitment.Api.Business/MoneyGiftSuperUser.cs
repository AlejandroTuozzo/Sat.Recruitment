using Sat.Recruitment.Api.Business.Interfaces;
using System;

namespace Sat.Recruitment.Api.Business
{
    public class MoneyGiftSuperUser : MoneyGiftHandler, IMoneyGiftHandler
    {
        public decimal GetGiftByUserType(decimal money)
        {
            if (money > 100)
            {
                return GetGift(money, Convert.ToDecimal(0.2));
            }

            return GetGift(money, 0);
        }
    }
}
