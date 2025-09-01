using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IQuestionService
	{
        List<QuestionDto> GetAllQuestions();
        List<QuestionDto> GetAllQuestionsByExcerciseId(int excerciseId);
        QuestionDto GetQuestionById(int id);
        QuestionDto CreateQuestion(CreateQuestionDto dto);
        void UpdateQuestion(int id, UpdateQuestionDto dto);

        void DeleteQuestion(int id);
    }
}

