using AngleSharp.Html.Parser;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using FSTW_backend.Models;
using FSTW_backend.Repositories.Admin;

namespace FSTW_backend.Services.SitesParsingServices
{
    public class KonturParsingService : IKonturParsingService
    {
        private Dictionary<string, string> descriptions = new Dictionary<string, string>()
        {
            ["Бэкенд-стажировка"] = "Бэкендер занимается разработкой и поддержкой серверной части веб-приложений и сервисов. ",
            ["Стажировка фронтенд‑разработчика"] = "Фронтендер занимается разработкой пользовательского интерфейса сайтов и приложений. ",
            ["Стажировка для аналитиков данных"] = "Аналитики данных работают в команде, которая помогает собирать данные для инфраструктурных, управленческих и маркетинговых проектов Контура.",
            ["Стажировка Data Science"] = "Специалист по сбору, анализу и визуализации данных",
            ["Стажировка в направлениях Системный анализ и Тестирование"] = "Аналитики данных работают в команде, которая помогает собирать данные для инфраструктурных, управленческих и маркетинговых проектов Контура."
        };

        private IAdminRepository _adminRepository;

        public KonturParsingService(AppDbContext context)
        {
            _adminRepository = new AdminRepository(context);
        }

        public async Task Parse()
        {
            var result = await GetDataFromSite();
            if (result is not null)
                await _adminRepository.AddVacanciesFromHh(result);
        }

        private async Task<List<Internship>> GetDataFromSite()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://kontur.ru/education/programs/city-5457");
            var source = driver.PageSource;
            var uriList = ParseMain("https://kontur.ru", source);

            var internships = new List<Internship>();
            var id = 0;

            var oldInternships = await _adminRepository.GetInternshipsByCompany("Контур");

            foreach (var uri in uriList)
            {
                driver.Navigate().GoToUrl(uri);
                var page = driver.PageSource;
                var result = ParseInternship(page);

                internships.Add(new Internship()
                {
                    CompanyName = "Контур",
                    Description = descriptions[result.Item1],
                    Direction = "IT и разработка",
                    Link = uri,
                    RequiredSkills = "Не указано",
                    SalaryFrom = 70000,
                    SalaryTo = 0,
                    Title = result.Item1,
                    WorkFormat = "Очно",
                    IdFromHh = (++id).ToString(),
                    isArchive = !result.Item2,
                });
            }
            driver.Quit();

            if (oldInternships.Count != 0)
            {
                foreach (var internship in oldInternships)
                {
                    var x = internships.FirstOrDefault(i => i.CompanyName == internship.CompanyName);

                    if (x is not null)
                        if (internship.isArchive != x.isArchive)
                            internship.isArchive = x.isArchive;
                }

                await _adminRepository.SaveChanges();
                return null;
            }

            return internships;
        }

        public List<string> ParseMain(string url, string content)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);

            var directions = document.QuerySelector(".edu-internship__directions");

            var info = directions.QuerySelectorAll(".internship-direction__title");

            var result = new List<string>();
            foreach (var item in info)
            {
                var path = item.GetAttribute("href");
                var directionUri = url + path;
                result.Add(directionUri);
            }
            return result;
        }

        public Tuple<string, bool> ParseInternship(string content)
        {
            var dict = new Dictionary<string, string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);

            var direction = document.QuerySelector(".content-head__title")?.TextContent.Trim();

            var status = document.QuerySelector(".edu-event-lead__item_status")?.TextContent.Trim();

            if (status == "Набор завершён" || status == "Оставьте заявку. Мы пришлем приглашение, когда откроем набор." ||
                status == "Оставьте свои контакты на этой странице, и мы сообщим вам о старте следующего набора")
                return new Tuple<string, bool>(direction, false);
            return new Tuple<string, bool>(direction, true);
        }
    }
}
