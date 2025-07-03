using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IQuestionService
	{
        List<QuestionDto> GetAllQuestions();
        QuestionDto GetQuestionById(int id);
        QuestionDto CreateQuestion(string title, int exerciseId);
        void UpdateQuestion(int id, string title, int exerciseId);
        void DeleteQuestion(int id);
    }
}

