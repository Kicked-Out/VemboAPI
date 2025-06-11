using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
namespace VemboAPI.Infrastructure.Services
{
	public class TopicService : ITopicService
    {
		private VemboDbContext _vemboDbContext;
		public TopicService(VemboDbContext vemboDbContext)
		{
			_vemboDbContext = vemboDbContext;
		}

		public Topic CreateTopic(string title, string description)
		{
			var topic = new Topic
			{
				Title = title,
				Description = description,

			};
			_vemboDbContext.Topics.Add(topic);
			_vemboDbContext.SaveChanges();
			return topic;
		}

		public void DeleteTopic(int id)
		{
			var topic = _vemboDbContext.Topics.Find(id);
			if (topic == null)
			{
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }
			_vemboDbContext.Topics.Remove(topic);
			_vemboDbContext.SaveChanges();
		}

		public List<Topic> GetAllTopics()
		{
			var collection = _vemboDbContext.Topics.ToList();
			foreach(var topic in collection)
			{
				topic.Title = topic.Title.ToUpper();
			}
			return collection;
		}

		public Topic GetTopicById(int id)
		{
			var topic = _vemboDbContext.Topics.Find(id);
			if(topic == null)
			{
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }
			return topic;
		}

		public void UpdateTopic(int id, string title, string description)
		{
			var topic = _vemboDbContext.Topics.Find(id);
			if (topic != null)
			{
				topic.Title = title;
				topic.Description = description;

				_vemboDbContext.Topics.Update(topic);
				_vemboDbContext.SaveChanges();
			
			}
            else
            {
                throw new KeyNotFoundException($"Topic with ID {id} not found.");
            }
        }
	}
}

