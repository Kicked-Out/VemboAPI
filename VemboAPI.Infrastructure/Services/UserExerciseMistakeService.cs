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

        public UserExerciseMistakeDto CreateMistake(CreateUserExerciseMistakeDto dto)
        {
            var mistake = new UserExerciseMistake
            {
                UserId = dto.UserId,
                ExerciseId = dto.ExerciseId,
                UserAnswer = dto.UserAnswer
            };

            _dbContext.UserExerciseMistakes.Add(mistake);
            _dbContext.SaveChanges();

            return _mapper.Map<UserExerciseMistakeDto>(mistake);
        }

        public void UpdateMistake(int id, UpdateUserExerciseMistakeDto dto)
        {
            var mistake = _dbContext.UserExerciseMistakes.Find(id);
            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            mistake.UserId = dto.UserId;
            mistake.ExerciseId = dto.ExerciseId;
            mistake.UserAnswer = dto.UserAnswer;

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
