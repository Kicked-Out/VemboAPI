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

        public UnitDto CreateUnit(CreateUnitDto dto)
        {
            if (!_dbContext.Topics.Any(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!_dbContext.GuideBooks.Any(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            var unit = _mapper.Map<Unit>(dto);
            unit.GuideBookId = dto.GuideBookId; // На випадок, якщо AutoMapper не встановить

            _dbContext.Units.Add(unit);
            _dbContext.SaveChanges();

            return _mapper.Map<UnitDto>(unit);
        }

        public void UpdateUnit(int id, UpdateUnitDto dto)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            if (!_dbContext.Topics.Any(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!_dbContext.GuideBooks.Any(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            _mapper.Map(dto, unit);
            unit.GuideBookId = dto.GuideBookId;

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

        public List<UnitDto> GetAllUnitsByTopicId(int topicId)
        {
            var units = _dbContext.Units
                .Where(unit => unit.TopicId == topicId)
                .ToList();

            return _mapper.Map<List<UnitDto>>(units);
        }
    }
}
