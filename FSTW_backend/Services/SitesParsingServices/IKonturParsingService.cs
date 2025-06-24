using FSTW_backend.Models;

namespace FSTW_backend.Services.SitesParsingServices
{
    public interface IKonturParsingService
    {
        public Task<List<Internship>> Parse();
    }
}
