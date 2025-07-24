using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public TopicService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<TopicDto> GetAllTopics()
        {
            var topics = _dbContext.Topics
                .Include(t => t.Units)
                .ToList();

            var result = _mapper.Map<List<TopicDto>>(topics);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].UnitsCount = topics[i].Units.Count;
            }

            return result;
        }

        public TopicDto GetTopicById(int id)
        {
            var topic = _dbContext.Topics
                .Include(t => t.Units)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            var dto = _mapper.Map<TopicDto>(topic);
            dto.UnitsCount = topic.Units.Count;
            return dto;
        }

        public TopicDto CreateTopic(TopicCreateDto dto)
        {
            if (!_dbContext.Periods.Any(p => p.Id == dto.PeriodId))
                throw new KeyNotFoundException($"Period with ID {dto.PeriodId} not found.");

            var topic = _mapper.Map<Topic>(dto);

            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();

            var result = _mapper.Map<TopicDto>(topic);
            result.UnitsCount = 0;
            return result;
        }


        public void UpdateTopic(int id, TopicUpdateDto dto)
        {
            var topic = _dbContext.Topics.Find(id);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            if (!_dbContext.Periods.Any(p => p.Id == dto.PeriodId))
                throw new KeyNotFoundException($"Period with ID {dto.PeriodId} not found.");

            _mapper.Map(dto, topic);
            _dbContext.SaveChanges();
        }


        public void DeleteTopic(int id)
        {
            var topic = _dbContext.Topics.Find(id);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            _dbContext.Topics.Remove(topic);
            _dbContext.SaveChanges();
        }
    }
}
