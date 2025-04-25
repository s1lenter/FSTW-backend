using System.ComponentModel.DataAnnotations;

namespace FSTW_backend.Filters
{
    public class PasswordValidator : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            var password = value as string;
            if (password is null)
                return FailedValidate("Поле пароль явялется обязательным");
            if (password.Length < 8 || password.Length > 30)
                return FailedValidate("Размер пароля от 8 до 30 символов");
            else if (!HaveDigits(password))
                return FailedValidate("В пароле должны быть цифры");
            else if (!HaveLetters(password))
                return FailedValidate("В пароле должны быть латинские буквы");
            return false;
        }

        public bool FailedValidate(string errorMessage)
        {
            ErrorMessage = errorMessage;
            return false;
        }

        public bool HaveDigits(string password)
        {
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9')
                    return true;
            }
            return false;
        }

        public bool HaveLetters(string password)
        {
            foreach (char c in password)
            {
                if (c >= 'A' && c <= 'z')
                    return true;
            }
            return false;
        }
    }
}
