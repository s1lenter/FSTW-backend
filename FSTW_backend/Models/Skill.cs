﻿namespace FSTW_backend.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
