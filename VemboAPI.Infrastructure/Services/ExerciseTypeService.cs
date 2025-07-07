using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseTypeService : IExerciseTypeService
    {
        private readonly VemboDbContext _dbContext;

        public ExerciseTypeService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ExerciseTypeDto> GetAllExerciseTypes()
        {
            return _dbContext.ExerciseTypes
                .Select(e => new ExerciseTypeDto
                {
                    Id = e.Id,
                    Title = e.Title
                })
                .ToList();
        }

        public ExerciseTypeDto GetExerciseTypeById(int id)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
            {
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");
            }

            return new ExerciseTypeDto
            {
                Id = exerciseType.Id,
                Title = exerciseType.Title
            };
        }

        public ExerciseTypeDto CreateExerciseType(string title)
        {
            var exerciseType = new ExerciseType
            {
                Title = title
            };

            _dbContext.ExerciseTypes.Add(exerciseType);
            _dbContext.SaveChanges();

            return new ExerciseTypeDto
            {
                Id = exerciseType.Id,
                Title = exerciseType.Title
            };
        }

        public void UpdateExerciseType(int id, string title)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
            {
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");
            }

            exerciseType.Title = title;

            _dbContext.ExerciseTypes.Update(exerciseType);
            _dbContext.SaveChanges();
        }

        public void DeleteExerciseType(int id)
        {
            var exerciseType = _dbContext.ExerciseTypes.Find(id);
            if (exerciseType == null)
            {
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");
            }

            _dbContext.ExerciseTypes.Remove(exerciseType);
            _dbContext.SaveChanges();
        }
    }
}
