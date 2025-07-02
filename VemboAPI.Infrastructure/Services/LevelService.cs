using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class LevelService : ILevelService
    {
        private readonly VemboDbContext _dbContext;

        public LevelService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LevelDto> GetAllLevels()
        {
            return _dbContext.Levels
                .Select(l => new LevelDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    UnitId = l.UnitId,
                    Order = l.Order
                })
                .ToList();
        }

        public LevelDto GetLevelById(int id)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {id} not found.");
            }

            return new LevelDto
            {
                Id = level.Id,
                Title = level.Title,
                UnitId = level.UnitId,
                Order = level.Order
            };
        }

        public LevelDto CreateLevel(string title, int unitId, int order)
        {
            var unit = _dbContext.Units.Find(unitId);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Unit with ID {unitId} not found.");
            }

            var level = new Level
            {
                Title = title,
                UnitId = unitId,
                Order = order
            };

            _dbContext.Levels.Add(level);
            _dbContext.SaveChanges();

            return new LevelDto
            {
                Id = level.Id,
                Title = level.Title,
                UnitId = level.UnitId,
                Order = level.Order
            };
        }

        public void UpdateLevel(int id, string title, int unitId, int order)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {id} not found.");
            }

            var unit = _dbContext.Units.Find(unitId);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Unit with ID {unitId} not found.");
            }

            level.Title = title;
            level.UnitId = unitId;
            level.Order = order;

            _dbContext.Levels.Update(level);
            _dbContext.SaveChanges();
        }

        public void DeleteLevel(int id)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {id} not found.");
            }

            _dbContext.Levels.Remove(level);
            _dbContext.SaveChanges();
        }
    }
}
