
using System.Collections.Generic;
using System.Linq;
using FACP.PasswordStrength.Service.Enums;

namespace FACP.PasswordStrength.Service.Helpers
{
    public static class StrengthExtension
    {        
        #region Fields

        private static int _score;


        #endregion

        #region Public Methods

        public static Strength PasswordStrength(this string password)
        {
            var score = password.GetStrengthScore();

            if (score <= 3)
                return Strength.VeryWeak;

            if (score > 3 && score <= 5)
                return Strength.Weak;

            if (score > 5 && score <= 10)
                return Strength.Reasonable;

            if (score > 10 && score <= 11)
                return Strength.Strong;

            if (score > 11)
                return Strength.VeryStrong;

            return Strength.VeryWeak;
        }

        public static int GetStrengthScore(this string password)
        {
            _score = 0;

            password
                .SetStrengthScoreLowerCase()
                .SetStrengthScoreDecimalDigitNumber()
                .SetStrengthScoreUpperCase()
                .SetStrengthScorePunctuation()
                .SetStrengthScoreSymbol()
                .SetStrengthScoreSeparator()
                .SetStrengthScoreSpecialChar();

            return _score;
        }

        #endregion

        #region Private Methods

        private static string SetStrengthScoreDecimalDigitNumber(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsDigit(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => char.IsDigit(character)))
                _score += distinctChars ? (int)StrengthScore.DecimalDigitNumber * 2 : (int)StrengthScore.DecimalDigitNumber;

            return password;
        }

        private static string SetStrengthScoreLowerCase(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsLower(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(c => char.IsLower(c)))
                _score += distinctChars ? (int)StrengthScore.LowerCase * 2 : (int)StrengthScore.LowerCase;

            return password;
        }

        private static string SetStrengthScoreUpperCase(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsUpper(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => char.IsUpper(character)))
                _score += distinctChars ? (int)StrengthScore.UpperCase * 2 : (int)StrengthScore.UpperCase;

            return password;
        }

        private static string SetStrengthScorePunctuation(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsPunctuation(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => char.IsPunctuation(character)))
                _score += distinctChars ? (int)StrengthScore.Punctuation * 2 : (int)StrengthScore.Punctuation;

            return password;
        }

        private static string SetStrengthScoreSymbol(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsSymbol(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => char.IsSymbol(character)))
                _score += distinctChars ? (int)StrengthScore.Symbol * 2 : (int)StrengthScore.Symbol;

            return password;
        }

        private static string SetStrengthScoreSeparator(this string password)
        {
            var distinctChars = password.Distinct().Where(character => char.IsSeparator(character)).ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => char.IsSeparator(character)))
                _score += distinctChars ? (int)StrengthScore.Separator * 2 : (int)StrengthScore.Separator;

            return password;
        }

        private static string SetStrengthScoreSpecialChar(this string password)
        {
            var distinctChars = password.Distinct().Where(character => character < ' ' || character > '~').ToList().MoreThanOneAndNotInOrder();

            if (password.Any(character => character < ' ' || character > '~'))
                _score += distinctChars ? (int)StrengthScore.SpecialChar * 2 : (int)StrengthScore.SpecialChar;

            return password;
        }

        private static bool MoreThanOneAndNotInOrder(this List<char> distincts)
        {
            var distinctCounter = 1;

            for (var item = 1; item < distincts.Count(); item++)
            {
                if (distincts[item] == distincts[item - 1] + 1)
                    distinctCounter++;
            }

            return distincts.Count() > distinctCounter;
        }

        #endregion

    }
}
