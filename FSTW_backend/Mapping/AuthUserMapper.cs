using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace FSTW_backend.Mapping
{
    public static class AuthUserMapper
    {
        public static User Map(UserRegisterDto userDto)
        {
            var user = new User();
            user.Login = userDto.Login;
            user.Email = userDto.Email;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, userDto.Password);
            user.CreatedDate = DateTime.UtcNow;
            return user;
        }
    }
}
