using FSTW_backend.Filters;
using System.ComponentModel.DataAnnotations;
using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FSTW_backend.Dto
{
    public class UserRegisterResponseDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordRepeat { get; set; }
    }
}
