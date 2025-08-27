using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserTopicProgressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int TopicId { get; set; }
		public int CompletedCount { get; set; }
	}
}

