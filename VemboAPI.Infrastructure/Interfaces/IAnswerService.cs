using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IAnswerService
	{
        List<AnswerDto> GetAllAnswers();
        AnswerDto GetAnswerById(int id);
        AnswerDto CreateAnswer(string title, bool isCorrect, int questionId);
        void UpdateAnswer(int id, string title, bool isCorrect, int questionId);
        void DeleteAnswer(int id);
    }
}

