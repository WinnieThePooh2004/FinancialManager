using Shared.ValueValidators.UserValuesValidator;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerTest.Tests.ValidatorsTests
{
    public class EmailValidatorTests
    {
        [Fact]
        public void TestCorrectEmail()
        {
            Assert.True(UserDetailsValidator.EmailValid("hunko.volodymyr@lll.kpi.ua"));
            //when you hack this email, please, do my homework
        }

        [Fact]
        public void TestEmailWithoutRateSign()
        {
            Assert.True(!UserDetailsValidator.EmailValid("hunko.volodymyrlll.kpi.ua"));
        }

        [Fact]
        public void TestEmailWithoutSymbolsAfterRateSign()
        {
            Assert.True(!UserDetailsValidator.EmailValid("hunko.volodymyr@"));
        }

        [Fact]
        public void TestEmailWithoutSymbolsAfterBeforeSign()
        {
            Assert.True(!UserDetailsValidator.EmailValid("@lll.kpi.ua"));
        }

    }
}
