﻿namespace FSTW_backend.Dto.AuthDto
{
    public class RefreshTokenRequestDto
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
