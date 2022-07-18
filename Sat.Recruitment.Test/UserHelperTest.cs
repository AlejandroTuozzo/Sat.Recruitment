using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Domain;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserHelperTest
    {
        private readonly IUserHelper _userHelper;

        public UserHelperTest()
        {
            _userHelper = new UserHelper();
        }

        [Fact]
        public void NormalizeEmail_Success()
        {
            string emailResult = _userHelper.NormalizeMail("cosme.fulanito+@gmail.com");
            Assert.Equal("cosmefulanito@gmail.com", emailResult);
        }

        [Fact]
        public void CalculateUserGift_UserTypeEmpty()
        {
            decimal money = 5;
            decimal? moneyResult = (decimal)_userHelper.CalculateUserGift(null, money);
            Assert.Equal(money, moneyResult);
        }

        [Fact]
        public void CalculateUserGift_NormalUserMoneyEmpty()
        {
            decimal? moneyResult = (decimal?)_userHelper.CalculateUserGift(UserType.Normal, null);
            Assert.Null(moneyResult);
        }

        [Fact]
        public void CalculateUserGift_NormalUserMoneyLessTen()
        {
            decimal money = 5;
            decimal moneyResult = (decimal)_userHelper.CalculateUserGift(UserType.Normal, money);
            Assert.Equal(money, moneyResult);
        }

        [Fact]
        public void CalculateUserGift_NormalUserMoneyBetweenTenAndOneHundred()
        {
            decimal money = 100;
            decimal moneyResult = (decimal)_userHelper.CalculateUserGift(UserType.Normal, money);
            Assert.Equal(180, moneyResult);
        }

        [Fact]
        public void CalculateUserGift_NormalUserMoneyGreaterTen()
        {
            decimal money = 200;
            decimal moneyResult = (decimal)_userHelper.CalculateUserGift(UserType.Normal, money);
            Assert.Equal(224, moneyResult);
        }

        [Fact]
        public void CalculateUserGift_SuperlUserMoneyEmpty()
        {
            decimal? moneyResult = (decimal?)_userHelper.CalculateUserGift(UserType.SuperUser, null);
            Assert.Null(moneyResult);
        }

        [Fact]
        public void CalculateUserGift_SuperUserMoneyGreaterTen()
        {
            decimal money = 200;
            decimal moneyResult = (decimal)_userHelper.CalculateUserGift(UserType.SuperUser, money);
            Assert.Equal(240, moneyResult);
        }

        [Fact]
        public void CalculateUserGift_PremiumUserMoneyEmpty()
        {
            decimal? moneyResult = (decimal?)_userHelper.CalculateUserGift(UserType.Premium, null);
            Assert.Null(moneyResult);
        }

        [Fact]
        public void CalculateUserGift_PremiumUserMoneyNotNull()
        {
            decimal money = 5;
            decimal moneyResult = (decimal)_userHelper.CalculateUserGift(UserType.Premium, money);
            Assert.Equal(15, moneyResult);
        }
    }
}
