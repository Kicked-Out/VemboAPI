using System.Collections.Generic;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        List<Topic> GetAllTopics(); 
        Topic GetTopicById(int id);
        void CreateTopic(string title, string description); 
        void UpdateTopic(int id, string title, string description); 
        void DeleteTopic(int id);
    }
}
