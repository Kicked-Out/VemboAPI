using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
	public interface IAnswerService
	{
        List<AnswerDto> GetAllAnswers();
        AnswerDto GetAnswerById(int id);
        AnswerDto CreateAnswer(CreateAnswerDto dto);
        void UpdateAnswer(int id, UpdateAnswerDto dto);

        void DeleteAnswer(int id);
    }
}

