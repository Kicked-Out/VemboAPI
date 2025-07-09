using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UnitService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UnitDto> GetAllUnits()
        {
            var units = _dbContext.Units.ToList();
            return _mapper.Map<List<UnitDto>>(units);
        }

        public UnitDto GetUnitById(int id)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            return _mapper.Map<UnitDto>(unit);
        }

        public UnitDto CreateUnit(string title, string description, int order, int topicId)
        {
            var topic = _dbContext.Topics.Find(topicId);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");

            var unit = new Unit
            {
                Title = title,
                Description = description,
                Order = order,
                TopicId = topicId
            };

            _dbContext.Units.Add(unit);
            _dbContext.SaveChanges();

            return _mapper.Map<UnitDto>(unit);
        }

        public void UpdateUnit(int id, string title, string description, int order, int topicId)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            var topic = _dbContext.Topics.Find(topicId);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");

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
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            _dbContext.Units.Remove(unit);
            _dbContext.SaveChanges();
        }
    }
}
