using FSTW_backend.Models;

namespace FSTW_backend.Repositories.SiteParsingRep
{
    public interface ISitesParsingRepository
    {
        public Task<Internship> GetInternshipWithLocalId(string id);
    }
}
