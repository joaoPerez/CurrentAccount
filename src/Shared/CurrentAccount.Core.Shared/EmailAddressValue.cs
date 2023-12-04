using CurrentAccount.Core.Shared.Result;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
    public record EmailAddressValue(string Email)
    {
        private static readonly string _notValidEmailErrorMessage = "The provided e-mail is not valid";
        private static readonly string _regexEmailAddress = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public static ResultModel<EmailAddressValue> Create(string email)
        {
            if (!IsValidEmail(email)) { return ResultModel<EmailAddressValue>.Failure(_notValidEmailErrorMessage); };
            return ResultModel<EmailAddressValue>.Success(new EmailAddressValue(email));
        }

        public static bool IsValidEmail(string email)
        {
            // Ensuring the regex will have a light work
            if (string.IsNullOrWhiteSpace(email) ||
               email.Length > 255 ||
               email.Count(x => x.Equals('@')) != 1
               )
            {
                return false;
            }

            if (email.IndexOf('@') == 0 || email.IndexOf('@') == email.Length ||
                email.IndexOf('.') == 0 || email.IndexOf('.') == email.Length)
            {
                return false;
            }

            // Regex Validation
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, _regexEmailAddress,
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
