using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;

namespace FSTW_backend.Services
{
    public class InternshipService : IInternshipService
    {
        private IMapper _mapper;
        private IIntershipRepository _repository;
        public InternshipService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _repository = new InternshipRepository(context);
        }

        public async Task<ResponseResult<List<InternshipDto>>> GetInternships(InternshipFiltersDto filters)
        {
            var internships = await _repository.GetInternships(filters);
            var internshipDtos = MapLists<Internship, InternshipDto>(internships);
            return ResponseResult<List<InternshipDto>>.Success(internshipDtos);
        }

        public async Task<ResponseResult<List<InternshipDto>>> GetFavoriteInternships(int userId)
        {
            var favorites = await _repository.GetFavorites(userId);
            var favInternships = await _repository.GetFavoriteInternships(favorites);
            var favInternshipDtos = MapLists<Internship, InternshipDto>(favInternships);
            return ResponseResult<List<InternshipDto>>.Success(favInternshipDtos);
        }

        public async Task<ResponseResult<Favorite>> AddFavoriteInternship(int userId, int internshipId)
        {
            var internship = await  _repository.GetInternship(internshipId);

            if (internship is null)
                return ResponseResult<Favorite>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });

            var sameFav = await _repository.GetFavoriteInternship(userId, internshipId);
            if (sameFav is not null)
                return ResponseResult<Favorite>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Эта стажировка уже в избранном"}
                });

            var favorite = new Favorite()
            {
                SavedAt = DateTime.UtcNow,
                InternshipId = internshipId,
                UserID = userId
            };

            await _repository.AddFavoriteInternship(favorite);
            return ResponseResult<Favorite>.Success(favorite);
        }

        public async Task<ResponseResult<int>> RemoveFavoriteInternship(int userId, int internshipId)
        {
            var fav = await _repository.GetFavoriteInternship(userId, internshipId);
            if (fav is null)
                return ResponseResult<int>.Failure(new List<Dictionary<string, string>>()
                {
                    new () {["Error"] = "Стажировки с таким Id не существует"}
                });
            return ResponseResult<int>.Success(await _repository.DeleteFavoriteInternship(fav));
        }

        private List<TResult> MapLists<TSource, TResult>(List<TSource> sourceList)
            where TResult : new()
        {
            var resultList = new List<TResult>();
            foreach (var item in sourceList)
            {
                var result = new TResult();
                _mapper.Map(item, result);
                resultList.Add(result);
            }

            return resultList;
        }
    }
}
