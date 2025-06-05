using System.Text.Json.Serialization;

namespace FSTW_backend.Dto.InternshipDto
{
    public class InternshipDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public string WorkFormat { get; set; }

        [JsonIgnore]
        public int SalaryFrom { get; set; }

        [JsonIgnore]
        public int SalaryTo { get; set; }

        public string SetSalary
        {
            get
            {

                if (SalaryFrom == 0 && SalaryTo == 0)
                    return "Зарплата не указана";
                return "Зарплата указана";
            }
        }
        public string Direction { get; set; }
        public string Link { get; set; }
        public bool IsArchive { get; set; }
        public bool IsFavorite { get; set; }
    }
}
