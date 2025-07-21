using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class LevelService : ILevelService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public LevelService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LevelDto> GetAllLevels()
        {
            var levels = _dbContext.Levels.ToList();
            return _mapper.Map<List<LevelDto>>(levels);
        }

        public LevelDto GetLevelById(int id)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            return _mapper.Map<LevelDto>(level);
        }

        public LevelDto CreateLevel(CreateLevelDto dto)
        {
            var unit = _dbContext.Units.Find(dto.UnitId);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            var level = new Level
            {
                Title = dto.Title,
                UnitId = dto.UnitId,
                Order = dto.Order
            };

            _dbContext.Levels.Add(level);
            _dbContext.SaveChanges();

            return _mapper.Map<LevelDto>(level);
        }

        public void UpdateLevel(int id, UpdateLevelDto dto)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            var unit = _dbContext.Units.Find(dto.UnitId);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            level.Title = dto.Title;
            level.UnitId = dto.UnitId;
            level.Order = dto.Order;

            _dbContext.Levels.Update(level);
            _dbContext.SaveChanges();
        }

        public void DeleteLevel(int id)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            _dbContext.Levels.Remove(level);
            _dbContext.SaveChanges();
        }
    }
}