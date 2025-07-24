using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserUnitProgressService : IUserUnitProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserUnitProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserUnitProgressDto> GetAllUserUnitProgress()
        {
            var unitProgressList = _dbContext.UserUnitProgresses.ToList();
            return _mapper.Map<List<UserUnitProgressDto>>(unitProgressList);
        }

        public UserUnitProgressDto GetUserUnitProgressById(int id)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            return _mapper.Map<UserUnitProgressDto>(progress);
        }

        public UserUnitProgressDto CreateUserUnitProgress(CreateUserUnitProgressDto dto)
        {
            var progress = _mapper.Map<UserUnitProgress>(dto);

            _dbContext.UserUnitProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserUnitProgressDto>(progress);
        }

        public void UpdateUserUnitProgress(int id, UpdateUserUnitProgressDto dto)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
            _dbContext.SaveChanges();
        }

        public void DeleteUserUnitProgress(int id)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            _dbContext.UserUnitProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }

}
