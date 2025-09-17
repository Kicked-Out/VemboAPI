using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IQuestionService
	{
        Task<List<QuestionDto>> GetAllQuestions();
        Task<List<QuestionDto>> GetAllQuestionsByExcerciseId(int exerciseId);
        Task<QuestionDto> GetQuestionById(int id);
        Task<QuestionDto> CreateQuestion(CreateQuestionDto dto);
        Task UpdateQuestion(int id, UpdateQuestionDto dto);

        Task DeleteQuestion(int id);
    }
}

