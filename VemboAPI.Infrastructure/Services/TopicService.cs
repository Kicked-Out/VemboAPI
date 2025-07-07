using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly VemboDbContext _dbContext;

        public TopicService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TopicDto> GetAllTopics()
        {
            return _dbContext.Topics
                .Include(t => t.Units)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    PeriodId = t.PeriodId,
                    UnitsCount = t.Units.Count
                })
                .ToList();
        }

        public TopicDto GetTopicById(int id)
        {
            var topic = _dbContext.Topics
                .Include(t => t.Units)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }

            return new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageUrl = topic.ImageUrl,
                PeriodId = topic.PeriodId,
                UnitsCount = topic.Units.Count
            };
        }

        public TopicDto CreateTopic(string title, string description, string imageUrl, int periodId)
        {
            var period = _dbContext.Periods.Find(periodId);
            if (period == null)
            {
                throw new KeyNotFoundException($"Period with ID {periodId} not found.");
            }

            var topic = new Topic
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                PeriodId = periodId
            };

            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();

            return new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageUrl = topic.ImageUrl,
                PeriodId = topic.PeriodId,
                UnitsCount = 0
            };
        }

        public void UpdateTopic(int id, string title, string description, string imageUrl, int periodId)
        {
            var topic = _dbContext.Topics.Find(id);
            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }

            var period = _dbContext.Periods.Find(periodId);
            if (period == null)
            {
                throw new KeyNotFoundException($"Period with ID {periodId} not found.");
            }

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
            {
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }

            _dbContext.Topics.Remove(topic);
            _dbContext.SaveChanges();
        }
    }
}
