using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserExerciseMistakeService : IUserExerciseMistakeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserExerciseMistakeService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserExerciseMistakeDto> GetAllMistakes()
        {
            var mistakes = _dbContext.UserExerciseMistakes.ToList();
            return _mapper.Map<List<UserExerciseMistakeDto>>(mistakes);
        }

        public UserExerciseMistakeDto GetMistakeById(int id)
        {
            var mistake = _dbContext.UserExerciseMistakes.Find(id);
            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            return _mapper.Map<UserExerciseMistakeDto>(mistake);
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

            return _mapper.Map<UserExerciseMistakeDto>(mistake);
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
