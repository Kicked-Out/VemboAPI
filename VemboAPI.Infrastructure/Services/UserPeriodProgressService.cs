using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserPeriodProgressService : IUserPeriodProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserPeriodProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserPeriodProgressDto> GetAllUserPeriodProgress()
        {
            var progresses = _dbContext.UserPeriodProgresses.ToList();
            return _mapper.Map<List<UserPeriodProgressDto>>(progresses);
        }

        public UserPeriodProgressDto GetUserPeriodProgressById(int id)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }

        public UserPeriodProgressDto CreateUserPeriodProgress(CreateUserPeriodProgressDto dto)
        {
            var progress = _mapper.Map<UserPeriodProgress>(dto);
            _dbContext.UserPeriodProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }


        public void UpdateUserPeriodProgress(int id, UpdateUserPeriodProgressDto dto)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
            _dbContext.SaveChanges();
        }



        public void DeleteUserPeriodProgress(int id)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            _dbContext.UserPeriodProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
