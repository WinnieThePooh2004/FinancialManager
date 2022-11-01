using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.ValueValidators.UserValuesValidator
{
    public static class UserDetailsValidator
    {
        public static bool EmailValid(string email) 
        {
            var validationRegexp = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|" +
                "\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0" +
                "e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:" +
                "(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?" +
                "[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-" +
                "\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");//this seems like shit, but it a regexp, so oi is shit
            return validationRegexp.IsMatch(email);
        }

        public static bool PasswordIsValid(string password)
        {
            if(password.Length < 8 || password.Length > 24) 
            {
                return false;
            }
            if(!password.Any(ch => char.IsLower(ch)))
            {
                return false;
            }
            if (!password.Any(ch => char.IsUpper(ch)))
            {
                return false;
            }
            if (!password.Any(ch => char.IsDigit(ch)))
            {
                return false;
            }
            return true;
        }
    }
}
