using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Demo.Common.Helpers
{
    public static class PasswordHelper
    {
        public static PasswordStrengthScoreEnum GetPasswordStrength(string password)
        {
            int score = 0;

            if (password != null)
            {
                switch(password.Length / 4)
                {
                    case 2:
                        score+=1;
                        break;
                    case 3:
                        score+=2;
                        break;
                    case 4:
                        score+=3;
                        break;
                    case 5:
                        score+=4;
                        break;
                }
            }

            return (PasswordStrengthScoreEnum)score;
        }

        public static bool IsPasswordValid(string password)
        {
            if (password != null 
                && password.Length >= 8
                && Regex.Match(password, @"[0-9]+(\.[0-9][0-9]?)?", RegexOptions.ECMAScript).Success
                && Regex.Match(password, @"(?=.*[a-z])", RegexOptions.ECMAScript).Success
                && Regex.Match(password, @"(?=.*[A-Z])", RegexOptions.ECMAScript).Success
                && Regex.Match(password, @"[^A-Za-z0-9]", RegexOptions.ECMAScript).Success)
                return true;
            else
                return false;
        }

        public static string GetPasswordStrengthColor(PasswordStrengthScoreEnum passwordStrengthScore)
        {
            switch (passwordStrengthScore)
            {
                case PasswordStrengthScoreEnum.NotValid:
                    return "#ff1100";
                case PasswordStrengthScoreEnum.Weak:
                    return "#b87114";
                case PasswordStrengthScoreEnum.Medium:
                    return "#637a15";
                case PasswordStrengthScoreEnum.Strong:
                    return "#98bf11";
                case PasswordStrengthScoreEnum.VeryStrong:
                    return "#c6ff00";
            }
            return "#FFFFFF";
        }
    }
}
