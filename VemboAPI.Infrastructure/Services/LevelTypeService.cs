using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class LevelTypeService : ILevelTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public LevelTypeService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LevelTypeDto> GetAll()
        {
            var levelTypes = _dbContext.LevelTypes.ToList();
            return _mapper.Map<List<LevelTypeDto>>(levelTypes);
        }

        public LevelTypeDto GetById(int id)
        {
            var levelType = _dbContext.LevelTypes.Find(id);
            if (levelType == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            return _mapper.Map<LevelTypeDto>(levelType);
        }

        public LevelTypeDto Create(CreateLevelTypeDto dto)
        {
            var entity = _mapper.Map<LevelType>(dto);
            _dbContext.LevelTypes.Add(entity);
            _dbContext.SaveChanges();
            return _mapper.Map<LevelTypeDto>(entity);
        }


        public void Update(int id, UpdateLevelTypeDto dto)
        {
            var entity = _dbContext.LevelTypes.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _mapper.Map(dto, entity);
            _dbContext.SaveChanges();
        }


        public void Delete(int id)
        {
            var levelType = _dbContext.LevelTypes.Find(id);
            if (levelType == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _dbContext.LevelTypes.Remove(levelType);
            _dbContext.SaveChanges();
        }
    }
}
