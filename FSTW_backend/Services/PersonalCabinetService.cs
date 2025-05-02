using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Mapping;
using FSTW_backend.Repositories;
using Profile = FSTW_backend.Models.Profile;

namespace FSTW_backend.Services
{
    public class PersonalCabinetService : IPersonalCabinetService
    {
        private IPersonalCabinetRepository _repository;
        private IMapper _mapper;
        public PersonalCabinetService(AppDbContext appDbContext, IMapper mapper)
        {
            _repository = new PersonalCabinetRepository(appDbContext, mapper);
            _mapper = mapper;
        }

        public async Task<Profile> CreatePersonalInfo(int userId, PersonalCabinetDto personalCabinetDto)
        {
            return await _repository.CreatePersonalCabinetInfo(userId, personalCabinetDto);
        }

        public async Task<PersonalCabinetDto> GetAllInfo(int userId)
        {
            var profile = await _repository.GetAllInfoAsync(userId);

            var pcDto = new PersonalCabinetDto();
            _mapper.Map(profile, pcDto);
            return pcDto;
        }
    }
}
