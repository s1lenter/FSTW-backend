using AngleSharp.Html.Parser;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace FSTW_backend.Services.SitesParsingServices
{
    public class KonturParsingService : ISiteParsingService
    {
        public List<string> Parse()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://kontur.ru/education/programs/city-5457");
            var source = driver.PageSource;
            var uriList = ParseMain("https://kontur.ru", source);
            var response = new List<string>();

            foreach (var uri in uriList)
            {
                driver.Navigate().GoToUrl(uri);
                var page = driver.PageSource;
                var result = ParseInternship(page);
                response.Add(result);
            }
            driver.Quit();

            return response;
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

        public string ParseInternship(string content)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);

            var direction = document.QuerySelector(".content-head__title")?.TextContent.Trim();

            return direction + ": " + document.QuerySelector(".edu-event-lead__item_status")?.TextContent.Trim();
        }
    }
}
