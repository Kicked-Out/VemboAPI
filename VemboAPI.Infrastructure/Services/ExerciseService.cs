using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExerciseService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ExerciseDto> GetAllExercise()
        {
            var exercises = _dbContext.Exercises.ToList();
            return _mapper.Map<List<ExerciseDto>>(exercises);
        }

        public ExerciseDto GetExerciseById(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            return _mapper.Map<ExerciseDto>(exercise);
        }

        public ExerciseDto CreateExercise(CreateExerciseDto dto)
        {
            var lesson = _dbContext.Lessons.Find(dto.LessonId);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = _dbContext.ExerciseTypes.Find(dto.ExerciseTypeId);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            var exercise = new Exercise
            {
                Title = dto.Title,
                LessonId = dto.LessonId,
                Difficulty = dto.Difficulty,
                ExerciseTypeId = dto.ExerciseTypeId,
                Order = dto.Order
            };

            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            return _mapper.Map<ExerciseDto>(exercise);
        }

        public void UpdateExercise(int id, UpdateExerciseDto dto)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            var lesson = _dbContext.Lessons.Find(dto.LessonId);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = _dbContext.ExerciseTypes.Find(dto.ExerciseTypeId);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            exercise.Title = dto.Title;
            exercise.LessonId = dto.LessonId;
            exercise.Difficulty = dto.Difficulty;
            exercise.ExerciseTypeId = dto.ExerciseTypeId;
            exercise.Order = dto.Order;

            _dbContext.Exercises.Update(exercise);
            _dbContext.SaveChanges();
        }


        public void DeleteExercise(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            _dbContext.Exercises.Remove(exercise);
            _dbContext.SaveChanges();
        }
    }
}
