﻿namespace FSTW_backend.Models
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Answer { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
