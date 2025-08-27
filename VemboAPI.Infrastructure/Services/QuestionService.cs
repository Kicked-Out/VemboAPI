using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<QuestionDto> GetAllQuestions()
        {
            var questions = _dbContext.Questions.ToList();
            return _mapper.Map<List<QuestionDto>>(questions);
        }

        public QuestionDto GetQuestionById(int id)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            return _mapper.Map<QuestionDto>(question);
        }

        public QuestionDto CreateQuestion(CreateQuestionDto dto)
        {
            if (!_dbContext.Exercises.Any(e => e.Id == dto.ExerciseId))
                throw new KeyNotFoundException($"Exercise with ID {dto.ExerciseId} not found.");

            var question = _mapper.Map<Question>(dto);

            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            return _mapper.Map<QuestionDto>(question);
        }


        public void UpdateQuestion(int id, UpdateQuestionDto dto)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            if (!_dbContext.Exercises.Any(e => e.Id == dto.ExerciseId))
                throw new KeyNotFoundException($"Exercise with ID {dto.ExerciseId} not found.");

            _mapper.Map(dto, question);
            _dbContext.SaveChanges();
        }



        public void DeleteQuestion(int id)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();
        }

        public List<QuestionDto> GetAllQuestionsByExcerciseId(int excerciseId)
        {
            var questions = _dbContext.Questions
                .ToList()
                .FindAll(question => question.ExerciseId == excerciseId);

            return _mapper.Map<List<QuestionDto>>(questions);
        }
    }
}
