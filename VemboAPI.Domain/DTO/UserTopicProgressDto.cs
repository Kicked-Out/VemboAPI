using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserTopicProgressDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int TopicId { get; set; }
		public bool isCompleted { get; set; }
	}
}

