using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IPeriodService
    {
        List<PeriodDto> GetAllPeriods();
        PeriodDto GetPeriodById(int id);
        PeriodDto CreatePeriod(CreatePeriodDto dto);
        void UpdatePeriod(int id, UpdatePeriodDto dto);
        void DeletePeriod(int id);
    }
}