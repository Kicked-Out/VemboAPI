using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestService : IQuestService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<QuestDto> GetAll()
        {
            var quests = _dbContext.Quests.ToList();
            return _mapper.Map<List<QuestDto>>(quests);
        }

        public QuestDto GetById(int id)
        {
            var quest = _dbContext.Quests.Find(id);
            if (quest == null)
                throw new KeyNotFoundException($"Quest with ID {id} not found.");
            return _mapper.Map<QuestDto>(quest);
        }

        public QuestDto Create(CreateQuestDto dto)
        {
            var quest = _mapper.Map<Quest>(dto);
            _dbContext.Quests.Add(quest);
            _dbContext.SaveChanges();
            return _mapper.Map<QuestDto>(quest);
        }

        public void Update(int id, UpdateQuestDto dto)
        {
            var quest = _dbContext.Quests.Find(id);
            if (quest == null)
                throw new KeyNotFoundException($"Quest with ID {id} not found.");
            _mapper.Map(dto, quest);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var quest = _dbContext.Quests.Find(id);
            if (quest == null)
                throw new KeyNotFoundException($"Quest with ID {id} not found.");
            _dbContext.Quests.Remove(quest);
            _dbContext.SaveChanges();
        }
    }
}
