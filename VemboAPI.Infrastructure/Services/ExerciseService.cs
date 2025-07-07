using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly VemboDbContext _dbContext;

        public ExerciseService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ExerciseDto> GetAllExercise()
        {
            return _dbContext.Exercises
                .Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    LessonId = e.LessonId,
                    Difficulty = e.Difficulty,
                    ExerciseTypeId = e.ExerciseTypeId,
                    Order = e.Order
                })
                .ToList();
        }

        public ExerciseDto GetExerciseById(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");
            }

            return new ExerciseDto
            {
                Id = exercise.Id,
                Title = exercise.Title,
                LessonId = exercise.LessonId,
                Difficulty = exercise.Difficulty,
                ExerciseTypeId = exercise.ExerciseTypeId,
                Order = exercise.Order
            };
        }

        public ExerciseDto CreateExercise(string title, int lessonId, bool difficulty, int exerciseTypeId, int order)
        {
            var lesson = _dbContext.Lessons.Find(lessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {lessonId} not found.");
            }

            var exerciseType = _dbContext.ExerciseTypes.Find(exerciseTypeId);
            if (exerciseType == null)
            {
                throw new KeyNotFoundException($"ExerciseType with ID {exerciseTypeId} not found.");
            }

            var exercise = new Exercise
            {
                Title = title,
                LessonId = lessonId,
                Difficulty = difficulty,
                ExerciseTypeId = exerciseTypeId,
                Order = order
            };

            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            return new ExerciseDto
            {
                Id = exercise.Id,
                Title = exercise.Title,
                LessonId = exercise.LessonId,
                Difficulty = exercise.Difficulty,
                ExerciseTypeId = exercise.ExerciseTypeId,
                Order = exercise.Order
            };
        }

        public void UpdateExercise(int id, string title, int lessonId, bool difficulty, int exerciseTypeId, int order)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");
            }

            var lesson = _dbContext.Lessons.Find(lessonId);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {lessonId} not found.");
            }

            var exerciseType = _dbContext.ExerciseTypes.Find(exerciseTypeId);
            if (exerciseType == null)
            {
                throw new KeyNotFoundException($"ExerciseType with ID {exerciseTypeId} not found.");
            }

            exercise.Title = title;
            exercise.LessonId = lessonId;
            exercise.Difficulty = difficulty;
            exercise.ExerciseTypeId = exerciseTypeId;
            exercise.Order = order;

            _dbContext.Exercises.Update(exercise);
            _dbContext.SaveChanges();
        }

        public void DeleteExercise(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");
            }

            _dbContext.Exercises.Remove(exercise);
            _dbContext.SaveChanges();
        }
    }
}
