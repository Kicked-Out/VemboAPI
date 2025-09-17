using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
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
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public AnswerService(
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

        public async Task<List<AnswerDto>> GetAllAnswers()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:answers:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var answers = await _dbContext.Answers.ToListAsync(); // синхронно ок
                var mapped = _mapper.Map<List<AnswerDto>>(answers);
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<AnswerDto> GetAnswerById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:answer:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var answer = await _dbContext.Answers.FindAsync(id);
                if (answer == null)
                    throw new KeyNotFoundException($"Answer with ID {id} not found.");

                var mapped = _mapper.Map<AnswerDto>(answer);
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<AnswerDto> CreateAnswer(CreateAnswerDto dto)
        {
            var question = await _dbContext.Questions.FindAsync(dto.QuestionId);

            if (question == null)
                throw new KeyNotFoundException($"Question with ID {dto.QuestionId} not found.");

            var answer = _mapper.Map<Answer>(dto);
            
            await _dbContext.Answers.AddAsync(answer);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу через нову версію

            return _mapper.Map<AnswerDto>(answer);
        }

        public async Task UpdateAnswer(int id, UpdateAnswerDto dto)
        {
            var answer = await _dbContext.Answers.FindAsync(id);

            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            var question = await _dbContext.Questions.FindAsync(dto.QuestionId);
            
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {dto.QuestionId} not found.");

            _mapper.Map(dto, answer);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteAnswer(int id)
        {
            var answer = await _dbContext.Answers.FindAsync(id);

            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            _dbContext.Answers.Remove(answer);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<AnswerDto>> GetAllAnswersByQuestionId(int questionId)
        {
            var answers = await _dbContext.Answers
                .Where(answer => answer.QuestionId == questionId)
                .ToListAsync();

            return _mapper.Map<List<AnswerDto>>(answers);
        }
    }
}