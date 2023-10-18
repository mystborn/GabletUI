using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GabletUI.Services.Validation
{
    public partial class RegexValidator : IValidationService
    {
        private const string PASSWORD_SYMBOLS = "!@#$%^&*()_+=.?-";

        [GeneratedRegex($"[{PASSWORD_SYMBOLS}]")]
        private static partial Regex HasSymbolsImpl();

        [GeneratedRegex("\\p{L}")]
        private static partial Regex HasLettersImpl();

        [GeneratedRegex("\\p{N}")]
        private static partial Regex HasNumbersImpl();

        [GeneratedRegex("\\b([\\p{N}\\p{L}])+\\b")]
        private static partial Regex HasOnlyLettersAndNumbersImpl();

        [GeneratedRegex("^[^@]+@[^@]+\\.[^@]+$")]
        private static partial Regex ValidEmailImpl();

        public static Regex HasSymbols { get; } = HasSymbolsImpl();

        public static Regex HasLetters { get; } = HasLettersImpl();

        public static Regex HasNumbers { get; } = HasNumbersImpl();

        public static Regex HasOnlyLettersAndNumbers { get; } = HasOnlyLettersAndNumbersImpl();

        public static Regex ValidEmail { get; } = ValidEmailImpl();

        public bool IsValidUsername(string username, out List<string> errors)
        {
            errors = new List<string>();
            var result = true;

            if (username.Length < 6)
            {
                result = false;
                errors.Add("Username must be at least 6 letters long");
            }

            if (username.Length > 20)
            {
                result = false;
                errors.Add("Username cannot exceed 20 letters");
            }

            if (!HasOnlyLettersAndNumbers.IsMatch(username))
            {
                result = false;
                errors.Add("Can only have letters, numbers, periods, and underscores in the username");
            }

            return result;
        }

        public bool IsValidPassword(string password, out List<string> errors)
        {
            errors = new List<string>();
            var result = true;

            if (password.Length < 8)
            {
                result = false;
                errors.Add("Password must be at least 8 letters long");
            }

            if (password.Length > 30)
            {
                result = false;
                errors.Add("Password length cannot exceed 30 letters");
            }

            if (!HasLetters.IsMatch(password))
            {
                result = false;
                errors.Add("Password must contain a letter.");
            }

            if (!HasNumbers.IsMatch(password))
            {
                result = false;
                errors.Add("Password must contain a number");
            }

            if (!HasSymbols.IsMatch(password))
            {
                result = false;
                errors.Add("Password must contain one of the following symbols: " + PASSWORD_SYMBOLS);
            }

            return result;
        }

        public bool IsValidEmail(string email, out List<string> errors)
        {
            errors = new List<string>();

            if(!ValidEmail.IsMatch(email))
            {
                errors.Add("Invalid email address");
                return false;
            }

            return true;
        }
    }
}
