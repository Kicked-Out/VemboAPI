using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AnswerDto> GetAllAnswers()
        {
            var answers = _dbContext.Answers.ToList();
            return _mapper.Map<List<AnswerDto>>(answers);
        }

        public AnswerDto GetAnswerById(int id)
        {
            var answer = _dbContext.Answers.Find(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            return _mapper.Map<AnswerDto>(answer);
        }

        public AnswerDto CreateAnswer(string title, bool isCorrect, int questionId)
        {
            var question = _dbContext.Questions.Find(questionId);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {questionId} not found.");

            var answer = new Answer
            {
                Title = title,
                isCorrect = isCorrect,
                QuestionId = questionId
            };

            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();

            return _mapper.Map<AnswerDto>(answer);
        }

        public void UpdateAnswer(int id, string title, bool isCorrect, int questionId)
        {
            var answer = _dbContext.Answers.Find(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            var question = _dbContext.Questions.Find(questionId);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {questionId} not found.");

            answer.Title = title;
            answer.isCorrect = isCorrect;
            answer.QuestionId = questionId;

            _dbContext.Answers.Update(answer);
            _dbContext.SaveChanges();
        }

        public void DeleteAnswer(int id)
        {
            var answer = _dbContext.Answers.Find(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            _dbContext.Answers.Remove(answer);
            _dbContext.SaveChanges();
        }
    }
}
