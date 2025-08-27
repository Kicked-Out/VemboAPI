using System;
namespace VemboAPI.Domain.Entities
{
	public class UserExerciseMistake
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int ExerciseId { get; set; }
		public string UserAnswer { get; set; }
		public User? User { get; set; }
		public Exercise? Exercise { get; set; }
	}
}

