using System;
namespace VemboAPI.Domain.Entities
{
	public class UserTopicProgress
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int TopicId { get; set; }
		public int CompletedCount { get; set; }
		public User? User { get; set; }
		public Topic? Topic { get; set; }
	}
}

