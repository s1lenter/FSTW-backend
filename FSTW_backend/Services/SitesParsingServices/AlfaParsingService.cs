using AngleSharp.Html.Parser;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using System.Text.Json.Serialization;
using System.Text.Json;
using FSTW_backend.Models;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FSTW_backend.Dto;
using FSTW_backend.Repositories.Admin;

namespace FSTW_backend.Services.SitesParsingServices
{
    public class AlfaParsingService : IAlfaParsingService
    {
        IAdminRepository _repository;
        public AlfaParsingService(AppDbContext context)
        {
            _repository = new AdminRepository(context);
        }

        public async Task GetAlfaData(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("https://alfabank.ru/api/v1/alfastudents-internships/internships?cityId.in%5B0%5D=275&directionId.in%5B0%5D=1&directionId.in%5B10%5D=25&directionId.in%5B11%5D=26&directionId.in%5B12%5D=27&directionId.in%5B13%5D=28&directionId.in%5B14%5D=31&directionId.in%5B1%5D=3&directionId.in%5B2%5D=5&directionId.in%5B3%5D=11&directionId.in%5B4%5D=14&directionId.in%5B5%5D=16&directionId.in%5B6%5D=17&directionId.in%5B7%5D=21&directionId.in%5B8%5D=23&directionId.in%5B9%5D=24&limit=40&offset=0&sort=updatedAt%3Aasc");

            var content = await response.Content.ReadAsStringAsync();

            var json = JsonSerializer.Deserialize<AlfaData>(content);

            var result = new List<Internship>();


            foreach (var internship in json.Data)
            {
                var direction = InternshipCategory.FindCategory(internship.Direction.Name);
                await SetRequirementsString(httpClient, internship);
                result.Add(new Internship()
                {
                    CompanyName = "Альфа Банк",
                    Description = internship.Description,
                    Direction = direction,
                    Link = internship.Link,
                    RequiredSkills = internship.Requirements,
                    SalaryFrom = 0,
                    SalaryTo = 0,
                    Title = internship.Name,
                    WorkFormat = internship.WorkFormat.Name == "Офис" ? "Очно" : internship.WorkFormat.Name,
                    IdFromHh = internship.ExternalId.ToString(),
                });
            }

            await _repository.AddVacanciesFromHh(result);
        }

        private async Task SetRequirementsString(HttpClient httpClient, AlfaInternship internship)
        {
            var link = $"https://alfabank.ru/api/v1/alfastudents-internships/internships/{internship.Uuid}";
            var internshipInfo = await httpClient.GetAsync(link);
            var infoContent = await internshipInfo.Content.ReadAsStringAsync();
            var infoJson = JsonSerializer.Deserialize<AlfaInternshipInfo>(infoContent);
            var requirementsList = infoJson.Requirements.Split("\n").Select(r =>
                string.Join(" ", r.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Trim());
            var requirements = string.Join(", ", requirementsList);
            internship.Requirements = requirements;
            internship.Description = infoJson.Direction.Description;
            internship.Link = link;
        }
    }
}
