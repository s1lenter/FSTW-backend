﻿using FSTW_backend.Dto.InternshipDto;
using FSTW_backend.Models;

namespace FSTW_backend.Repositories.Internships
{
    public interface IIntershipRepository
    {
        public Task<List<Internship>> GetInternships(InternshipFiltersDto filters);

        public Task<List<Internship>> GetInternships();

        public Task<List<Favorite>> GetFavorites(int userId);

        public Task<List<Internship>> GetFavoriteInternships(List<Favorite> favorites);

        public Task<Favorite> GetFavoriteInternship(int userId, int internshipId);

        public Task AddFavoriteInternship(Favorite favorite);

        public Task<int> DeleteFavoriteInternship(Favorite favorite);

        public Task<Internship> GetInternship(int internshipId);

        public Task<Profile> GetUserProfile(int userId);
    }
}
