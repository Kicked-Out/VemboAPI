using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly VemboDbContext _dbContext;

        public UnitService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UnitDto> GetAllUnits()
        {
            return _dbContext.Units
                .Select(u => new UnitDto
                {
                    Id = u.Id,
                    Title = u.Title,
                    Description = u.Description,
                    Order = u.Order,
                    TopicId = u.TopicId
                })
                .ToList();
        }

        public UnitDto GetUnitById(int id)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Unit with ID {id} not found.");
            }

            return new UnitDto
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                Order = unit.Order,
                TopicId = unit.TopicId
            };
        }

        public UnitDto CreateUnit(string title, string description, int order, int topicId)
        {
            var topic = _dbContext.Topics.Find(topicId);
            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");
            }

            var unit = new Unit
            {
                Title = title,
                Description = description,
                Order = order,
                TopicId = topicId
            };

            _dbContext.Units.Add(unit);
            _dbContext.SaveChanges();

            return new UnitDto
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                Order = unit.Order,
                TopicId = unit.TopicId
            };
        }

        public void UpdateUnit(int id, string title, string description, int order, int topicId)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Unit with ID {id} not found.");
            }

            var topic = _dbContext.Topics.Find(topicId);
            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");
            }

            unit.Title = title;
            unit.Description = description;
            unit.Order = order;
            unit.TopicId = topicId;

            _dbContext.Units.Update(unit);
            _dbContext.SaveChanges();
        }

        public void DeleteUnit(int id)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Unit with ID {id} not found.");
            }

            _dbContext.Units.Remove(unit);
            _dbContext.SaveChanges();
        }
    }
}
