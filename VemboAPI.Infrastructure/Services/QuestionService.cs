using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly VemboDbContext _dbContext;

        public QuestionService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<QuestionDto> GetAllQuestions()
        {
            return _dbContext.Questions
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    ExerciseId = q.ExerciseId
                })
                .ToList();
        }

        public QuestionDto GetQuestionById(int id)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }

            return new QuestionDto
            {
                Id = question.Id,
                Title = question.Title,
                ExerciseId = question.ExerciseId
            };
        }

        public QuestionDto CreateQuestion(string title, int exerciseId)
        {
            var exercise = _dbContext.Exercises.Find(exerciseId);
            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {exerciseId} not found.");
            }

            var question = new Question
            {
                Title = title,
                ExerciseId = exerciseId
            };

            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            return new QuestionDto
            {
                Id = question.Id,
                Title = question.Title,
                ExerciseId = question.ExerciseId
            };
        }

        public void UpdateQuestion(int id, string title, int exerciseId)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }

            var exercise = _dbContext.Exercises.Find(exerciseId);
            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {exerciseId} not found.");
            }

            question.Title = title;
            question.ExerciseId = exerciseId;

            _dbContext.Questions.Update(question);
            _dbContext.SaveChanges();
        }

        public void DeleteQuestion(int id)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }

            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();
        }
    }
}
