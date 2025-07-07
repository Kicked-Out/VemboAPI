using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IPeriodService
    {
        List<PeriodDto> GetAllPeriods();
        PeriodDto GetPeriodById(int id);
        void CreatePeriod(string title, string description, string imageUrl);
        void UpdatePeriod(int id, string title, string description, string imageUrl);
        void DeletePeriod(int id);
    }
}