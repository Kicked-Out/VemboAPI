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

        public List<AnswerDto> GetAllAnswers()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:answers:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var answers = _dbContext.Answers.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<AnswerDto>>(answers);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public AnswerDto GetAnswerById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:answer:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var answer = _dbContext.Answers.Find(id);
                if (answer == null)
                    throw new KeyNotFoundException($"Answer with ID {id} not found.");

                var mapped = _mapper.Map<AnswerDto>(answer);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public AnswerDto CreateAnswer(CreateAnswerDto dto)
        {
            var question = _dbContext.Questions.Find(dto.QuestionId);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {dto.QuestionId} not found.");

            var answer = _mapper.Map<Answer>(dto);
            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу через нову версію

            return _mapper.Map<AnswerDto>(answer);
        }

        public void UpdateAnswer(int id, UpdateAnswerDto dto)
        {
            var answer = _dbContext.Answers.Find(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            var question = _dbContext.Questions.Find(dto.QuestionId);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {dto.QuestionId} not found.");

            _mapper.Map(dto, answer);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteAnswer(int id)
        {
            var answer = _dbContext.Answers.Find(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            _dbContext.Answers.Remove(answer);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
