using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IAnswerService
	{
        Task<List<AnswerDto>> GetAllAnswers();
        Task<AnswerDto> GetAnswerById(int id);
        Task<List<AnswerDto>> GetAllAnswersByQuestionId(int questionId);
        Task<AnswerDto> CreateAnswer(CreateAnswerDto dto);
        Task UpdateAnswer(int id, UpdateAnswerDto dto);
        Task DeleteAnswer(int id);
    }
}

