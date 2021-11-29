using System;
using System.Linq;

namespace prescreminder.Utilities
{
    public static class StringValidationsUtility
    {
        public static bool IsAlphaNumeric(this string val) => val.All(char.IsLetterOrDigit);

        public static bool IsEmailAddress(this string val)
        {
            if (val.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(val);
                return string.Equals(emailAddress.Address, val, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
