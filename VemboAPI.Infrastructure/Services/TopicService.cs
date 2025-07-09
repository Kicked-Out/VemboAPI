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

        public TopicDto CreateTopic(string title, string description, string imageUrl, int periodId)
        {
            var period = _dbContext.Periods.Find(periodId);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {periodId} not found.");

            var topic = new Topic
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                PeriodId = periodId
            };

            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();

            var dto = _mapper.Map<TopicDto>(topic);
            dto.UnitsCount = 0;
            return dto;
        }

        public void UpdateTopic(int id, string title, string description, string imageUrl, int periodId)
        {
            var topic = _dbContext.Topics.Find(id);
            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            var period = _dbContext.Periods.Find(periodId);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {periodId} not found.");

            topic.Title = title;
            topic.Description = description;
            topic.ImageUrl = imageUrl;
            topic.PeriodId = periodId;

            _dbContext.Topics.Update(topic);
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
