using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        List<TopicDto> GetAllTopics();
        TopicDto GetTopicById(int id);
        TopicDto CreateTopic(string title, string description, string imageUrl, int periodId);
        void UpdateTopic(int id, string title, string description, string imageUrl, int periodId);
        void DeleteTopic(int id);
    }
}
