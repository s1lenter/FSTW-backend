using FSTW_backend.Filters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSTW_backend.Dto.AuthDto
{
    public class UserRegisterRequestDto
    {
        public string Login { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [PasswordValidator]
        public string? Password { get; set; }

        public string? PasswordRepeat { get; set; }
    }
}
