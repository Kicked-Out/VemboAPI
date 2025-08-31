using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class DailyQuestService : IDailyQuestService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public DailyQuestService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DailyQuestDto> GetAll()
        {
            var dailyQuests = _dbContext.DailyQuests.Include(d => d.Quest).ToList();
            return _mapper.Map<List<DailyQuestDto>>(dailyQuests);
        }

        public DailyQuestDto GetById(int id)
        {
            var dailyQuest = _dbContext.DailyQuests.Include(d => d.Quest).FirstOrDefault(d => d.Id == id);
            if (dailyQuest == null)
                throw new KeyNotFoundException($"Daily quest with ID {id} not found.");
            return _mapper.Map<DailyQuestDto>(dailyQuest);
        }

        public DailyQuestDto Create(CreateDailyQuestDto dto)
        {
            if (!_dbContext.Quests.Any(q => q.Id == dto.QuestId))
                throw new KeyNotFoundException($"Quest with ID {dto.QuestId} not found.");
            var dailyQuest = _mapper.Map<DailyQuest>(dto);
            _dbContext.DailyQuests.Add(dailyQuest);
            _dbContext.SaveChanges();
            return _mapper.Map<DailyQuestDto>(dailyQuest);
        }

        public void Update(int id, UpdateDailyQuestDto dto)
        {
            var dailyQuest = _dbContext.DailyQuests.Find(id);
            if (dailyQuest == null)
                throw new KeyNotFoundException($"Daily quest with ID {id} not found.");
            if (!_dbContext.Quests.Any(q => q.Id == dto.QuestId))
                throw new KeyNotFoundException($"Quest with ID {dto.QuestId} not found.");
            _mapper.Map(dto, dailyQuest);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dailyQuest = _dbContext.DailyQuests.Find(id);
            if (dailyQuest == null)
                throw new KeyNotFoundException($"Daily quest with ID {id} not found.");
            _dbContext.DailyQuests.Remove(dailyQuest);
            _dbContext.SaveChanges();
        }
    }
}
