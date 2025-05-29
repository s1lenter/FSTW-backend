using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace FSTW_backend.Dto
{
    public class HhResponseData
    {
        [JsonPropertyName("items")]
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("employer")]
        public Company Company { get; set; }

        [JsonPropertyName("snippet")]
        public Info Info { get; set; }

        [JsonPropertyName("work_format")]
        public List<WorkFormat> WorkFormats { get; set; }

        [JsonPropertyName("alternate_url")]
        public string Link { get; set; }

        [JsonPropertyName("salary")]
        public Salary Salary { get; set; }

        [JsonPropertyName("professional_roles")]
        public List<ProfessionalRole> ProfessionalRoles { get; set; }

        public SkillsInfo? SkillsInfo { get; set; }
    }

    public class ProfessionalRole
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Salary
    {
        [JsonPropertyName("from")] 
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }
    }

    public class WorkFormat
    {
        private string name;

        [JsonPropertyName("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Contains("На месте работодателя"))
                    name = "Очно";
                else
                    name = value;
            }
        }
    }

    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("requirement")]
        public string Description { get; set; }
    }

    public class SkillsInfo
    {
        [JsonPropertyName("key_skills")]
        public List<VacancySkill>? Skills { get; set; }
    }

    public class VacancySkill
    {
        [JsonPropertyName("name")]
        public string Skill { get; set; }
    }
}
