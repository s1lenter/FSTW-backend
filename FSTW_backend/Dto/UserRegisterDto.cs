﻿namespace FSTW_backend.Dto
{
    public class UserRegisterDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string PasswordRepeat { get; set; }
    }
}
