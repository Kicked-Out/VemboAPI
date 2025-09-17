using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        Task<List<TopicDto>> GetAllTopics();
        Task<TopicDto> GetTopicById(int id);
        Task<TopicDto> CreateTopic(TopicCreateDto dto);
        Task UpdateTopic(int id, TopicUpdateDto dto);
        Task DeleteTopic(int id);
    }
}
