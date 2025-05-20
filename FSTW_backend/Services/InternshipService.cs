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

        public ResponseResult<List<InternshipDto>> GetAllInternships()
        {
            var internships = _repository.GetAllInternships();
            var internshipDtos = MapLists<Internship, InternshipDto>(internships);
            return ResponseResult<List<InternshipDto>>.Success(internshipDtos);
        }

        public Task<ResponseResult<InternshipDto>> GetInternshipById(string id)
        {
            throw new NotImplementedException();
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
