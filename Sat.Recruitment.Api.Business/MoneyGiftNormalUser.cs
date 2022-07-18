using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Api.Business
{
    public class MoneyGiftNormalUser : MoneyGiftHandler, IMoneyGiftHandler
    {
        public decimal GetGiftByUserType(decimal money)
        {
            if (money > 100)
            {
                return GetGift(money, Convert.ToDecimal(0.12));
            }
             
            if (money > 10)
            {
                return GetGift(money, Convert.ToDecimal(0.8));
            }

            return GetGift(money, 0);
        }
    }
}
