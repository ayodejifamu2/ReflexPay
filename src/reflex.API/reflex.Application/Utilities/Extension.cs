using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Application.Utilities
{
    public static class Extension
    {
        public static bool IsStrongPassword(string password)
        {
            // Check if the password is at least 8 characters long
            if (password.Length < 8)
            {
                return false;
            }

            // Check if the password contains at least one number
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Check if the password contains at least one capital letter
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Check if the password contains at least one small letter
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            // Check if the password contains at least one special character
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            return true;
        }
        public static bool IsValidCVV(int cvv)
        {
            return cvv > 0 && cvv.ToString().Length == 3;
        }
        public static bool IsValidExpiryDate(string expiryDate)
        {
            // Convert expiry date string to DateTime and compare with current date
            if (DateTime.TryParse(expiryDate, out DateTime expiryDateTime))
            {
                return expiryDateTime > DateTime.Now;
            }
            else
            {
                // Invalid expiry date format
                return false;
            }
        }
        public static string GenerateOTP()
        {
            Random random = new Random();
            int otpNumber = random.Next(100000, 999999); // Generate a random number between 100000 and 999999
            return otpNumber.ToString();
        }
    }
}
