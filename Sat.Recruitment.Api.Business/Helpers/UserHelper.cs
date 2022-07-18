using System;
using System.Collections.Generic;
using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Domain;

namespace Sat.Recruitment.Api.Business
{
    public class UserHelper : IUserHelper
    {
        private readonly Dictionary<UserType, IMoneyGiftHandler> Handlers = new Dictionary<UserType, IMoneyGiftHandler>()
        {
            {UserType.Normal, new MoneyGiftNormalUser()},
            {UserType.SuperUser, new MoneyGiftSuperUser()},
            {UserType.Premium, new MoneyGiftPremiumUser()}
        };

        /// <summary>
        /// Calculate gift using the appropriate implementation
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public decimal? CalculateUserGift(UserType? userType, decimal? money)
        {
            if (money == null || userType == null) return money;
            
            return Handlers[userType.Value].GetGiftByUserType((decimal)money);
        }

        public string NormalizeMail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            aux[0] = aux[0].Replace(".", "").Replace("+", "");

            return string.Join("@", aux);
        }
    }
}
