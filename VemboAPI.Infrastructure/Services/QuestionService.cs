using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public QuestionService(
            VemboDbContext dbContext,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService ver)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
            _ver = ver;
        }

        public List<QuestionDto> GetAllQuestions()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:questions:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var questions = _dbContext.Questions.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<QuestionDto>>(questions);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public QuestionDto GetQuestionById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:question:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var question = _dbContext.Questions.Find(id);
                if (question == null)
                    throw new KeyNotFoundException($"Question with ID {id} not found.");

                var mapped = _mapper.Map<QuestionDto>(question);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public QuestionDto CreateQuestion(CreateQuestionDto dto)
        {
            if (!_dbContext.Exercises.Any(e => e.Id == dto.ExerciseId))
                throw new KeyNotFoundException($"Exercise with ID {dto.ExerciseId} not found.");

            var question = _mapper.Map<Question>(dto);

            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу

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

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteQuestion(int id)
        {
            var question = _dbContext.Questions.Find(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
