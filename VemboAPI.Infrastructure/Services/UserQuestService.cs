using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserQuestService : IUserQuestService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserQuestService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserQuestDto> GetAll()
        {
            var userQuests = _dbContext.UserQuests.Include(uq => uq.Quest).ToList();
            return _mapper.Map<List<UserQuestDto>>(userQuests);
        }

        public UserQuestDto GetById(int id)
        {
            var userQuest = _dbContext.UserQuests.Include(uq => uq.Quest).FirstOrDefault(uq => uq.Id == id);
            if (userQuest == null)
                throw new KeyNotFoundException($"User quest with ID {id} not found.");
            return _mapper.Map<UserQuestDto>(userQuest);
        }

        public UserQuestDto Create(CreateUserQuestDto dto)
        {
            if (!_dbContext.Users.Any(u => u.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            if (!_dbContext.Quests.Any(q => q.Id == dto.QuestId))
                throw new KeyNotFoundException($"Quest with ID {dto.QuestId} not found.");
            var userQuest = _mapper.Map<UserQuest>(dto);
            _dbContext.UserQuests.Add(userQuest);
            _dbContext.SaveChanges();
            return _mapper.Map<UserQuestDto>(userQuest);
        }

        public void Update(int id, UpdateUserQuestDto dto)
        {
            var userQuest = _dbContext.UserQuests.Find(id);
            if (userQuest == null)
                throw new KeyNotFoundException($"User quest with ID {id} not found.");
            _mapper.Map(dto, userQuest);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var userQuest = _dbContext.UserQuests.Find(id);
            if (userQuest == null)
                throw new KeyNotFoundException($"User quest with ID {id} not found.");
            _dbContext.UserQuests.Remove(userQuest);
            _dbContext.SaveChanges();
        }
    }
}
