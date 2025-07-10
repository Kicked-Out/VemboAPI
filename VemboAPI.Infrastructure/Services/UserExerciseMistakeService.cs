using System.Collections.Generic;
using System.Linq;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Services
{
    public class UserExerciseMistakeService : IUserExerciseMistakeService
    {
        private readonly VemboDbContext _dbContext;

        public UserExerciseMistakeService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserExerciseMistakeDto> GetAllMistakes()
        {
            return _dbContext.UserExerciseMistakes
                .Select(m => new UserExerciseMistakeDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    ExerciseId = m.ExerciseId,
                    UserAnswer = m.UserAnswer
                })
                .ToList();
        }

        public UserExerciseMistakeDto GetMistakeById(int id)
        {
            var mistake = _dbContext.UserExerciseMistakes.Find(id);
            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            return new UserExerciseMistakeDto
            {
                Id = mistake.Id,
                UserId = mistake.UserId,
                ExerciseId = mistake.ExerciseId,
                UserAnswer = mistake.UserAnswer
            };
        }

        public UserExerciseMistakeDto CreateMistake(int userId, int exerciseId, string userAnswer)
        {
            var mistake = new UserExerciseMistake
            {
                UserId = userId,
                ExerciseId = exerciseId,
                UserAnswer = userAnswer
            };

            _dbContext.UserExerciseMistakes.Add(mistake);
            _dbContext.SaveChanges();

            return new UserExerciseMistakeDto
            {
                Id = mistake.Id,
                UserId = mistake.UserId,
                ExerciseId = mistake.ExerciseId,
                UserAnswer = mistake.UserAnswer
            };
        }

        public void UpdateMistake(int id, int userId, int exerciseId, string userAnswer)
        {
            var mistake = _dbContext.UserExerciseMistakes.Find(id);
            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            mistake.UserId = userId;
            mistake.ExerciseId = exerciseId;
            mistake.UserAnswer = userAnswer;

            _dbContext.UserExerciseMistakes.Update(mistake);
            _dbContext.SaveChanges();
        }

        public void DeleteMistake(int id)
        {
            var mistake = _dbContext.UserExerciseMistakes.Find(id);
            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            _dbContext.UserExerciseMistakes.Remove(mistake);
            _dbContext.SaveChanges();
        }
    }
}
