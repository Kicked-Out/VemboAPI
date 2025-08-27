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
            if (!_dbContext.Units.Any(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!_dbContext.LevelTypes.Any(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            var level = _mapper.Map<Level>(dto);
            level.LevelTypeId = dto.LevelTypeId;

            _dbContext.Levels.Add(level);
            _dbContext.SaveChanges();

            return _mapper.Map<LevelDto>(level);
        }

        public void UpdateLevel(int id, UpdateLevelDto dto)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            if (!_dbContext.Units.Any(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!_dbContext.LevelTypes.Any(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            _mapper.Map(dto, level);
            level.LevelTypeId = dto.LevelTypeId;

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

        public List<LevelDto> GetAllLevelsByUnitId(int unitId)
        {
            var levels = _dbContext.Levels
                .Where(level => level.UnitId == unitId)
                .ToList();

            return _mapper.Map<List<LevelDto>>(levels);
        }
    }
}
