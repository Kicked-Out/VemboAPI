using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        List<TopicDto> GetAllTopics();
        TopicDto GetTopicById(int id);
        TopicDto CreateTopic(TopicCreateDto dto);
        void UpdateTopic(int id, TopicUpdateDto dto);
        void DeleteTopic(int id);
    }
}
