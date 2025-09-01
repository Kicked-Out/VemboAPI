using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserExerciseMistakeDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int ExerciseId { get; set; }
		public string UserAnswer { get; set; }
	}
}

