using FSTW_backend.Models;

namespace FSTW_backend.Services.SitesParsingServices
{
    public interface IAlfaParsingService
    {
        public Task GetAlfaData(HttpClient httpClient);
    }
}
