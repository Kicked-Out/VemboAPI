using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<QuestionDto>> GetAllQuestions()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:questions:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var questions = await _dbContext.Questions.ToListAsync(); // синхронно ок
                var mapped = _mapper.Map<List<QuestionDto>>(questions);
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<QuestionDto> GetQuestionById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:question:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var question = await _dbContext.Questions.FindAsync(id);
                if (question == null)
                    throw new KeyNotFoundException($"Question with ID {id} not found.");

                var mapped = _mapper.Map<QuestionDto>(question);
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<QuestionDto> CreateQuestion(CreateQuestionDto dto)
        {
            if (!await _dbContext.Exercises.AnyAsync(e => e.Id == dto.ExerciseId))
                throw new KeyNotFoundException($"Exercise with ID {dto.ExerciseId} not found.");

            var question = _mapper.Map<Question>(dto);

            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу

            return _mapper.Map<QuestionDto>(question);
        }

        public async Task UpdateQuestion(int id, UpdateQuestionDto dto)
        {
            var question = await _dbContext.Questions.FindAsync(id);

            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            if (!await _dbContext.Exercises.AnyAsync(e => e.Id == dto.ExerciseId))
                throw new KeyNotFoundException($"Exercise with ID {dto.ExerciseId} not found.");

            _mapper.Map(dto, question);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _dbContext.Questions.FindAsync(id);

            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<QuestionDto>> GetAllQuestionsByExcerciseId(int exerciseId)
        {
            var questions = await _dbContext.Questions
                .Where(question => question.ExerciseId == exerciseId)
                .ToListAsync();

            return _mapper.Map<List<QuestionDto>>(questions);
        }
    }
}
