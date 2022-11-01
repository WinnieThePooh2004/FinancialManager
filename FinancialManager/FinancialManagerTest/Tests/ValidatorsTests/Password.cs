using Shared.ValueValidators.UserValuesValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerTest.Tests.ValidatorsTests
{
    public class Password
    {
        [Fact]
        public void TestCorrectPassword()
        {
            Assert.True(UserDetailsValidator.PasswordIsValid("ABCdef56"));
        }

        [Fact]
        public void TestShortPassword()
        {
            Assert.True(!UserDetailsValidator.PasswordIsValid("A"));
        }

        [Fact]
        public void TestLongPassword()
        {
            Assert.True(!UserDetailsValidator.PasswordIsValid("Adiejdiojeoijdewiojdoiewjdoiewjdoiwjedoijweodijewoidj"));
        }

        [Fact]
        public void TestPasswordWithoutLowerLetters()
        {
            Assert.True(!UserDetailsValidator.PasswordIsValid("66878787687687687"));
        }

        [Fact]
        public void TestPasswordWithoutUpperLetters()
        {
            Assert.True(!UserDetailsValidator.PasswordIsValid("jeidjiwjd29129182"));
        }

        [Fact]
        public void TestPasswordWithoutDigits()
        {
            Assert.True(!UserDetailsValidator.PasswordIsValid("eudeuideiudiueIJIOJJ"));
        }
    }
}
