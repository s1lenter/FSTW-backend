﻿namespace FSTW_backend.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Place { get; set; }
        public string Specialization { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
