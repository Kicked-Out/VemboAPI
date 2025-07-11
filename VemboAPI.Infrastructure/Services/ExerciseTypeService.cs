using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseTypeService : IExerciseTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExerciseTypeService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ExerciseTypeDto> GetAllExerciseTypes()
        {
            var exerciseTypes = _dbContext.ExerciseTypes.ToList();
            return _mapper.Map<List<ExerciseTypeDto>>(exerciseTypes);
        }

        public ExerciseTypeDto GetExerciseTypeById(int id)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            return _mapper.Map<ExerciseTypeDto>(exerciseType);
        }

        public ExerciseTypeDto CreateExerciseType(string title)
        {
            var exerciseType = new ExerciseType { Title = title };

            _dbContext.ExerciseTypes.Add(exerciseType);
            _dbContext.SaveChanges();

            return _mapper.Map<ExerciseTypeDto>(exerciseType);
        }

        public void UpdateExerciseType(int id, string title)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            exerciseType.Title = title;

            _dbContext.ExerciseTypes.Update(exerciseType);
            _dbContext.SaveChanges();
        }

        public void DeleteExerciseType(int id)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            _dbContext.ExerciseTypes.Remove(exerciseType);
            _dbContext.SaveChanges();
        }
    }
}
