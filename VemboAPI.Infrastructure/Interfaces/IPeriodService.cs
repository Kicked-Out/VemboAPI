using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IPeriodService
    {
        Task<List<PeriodDto>> GetAllPeriods();
        Task<PeriodDto> GetPeriodById(int id);
        Task<PeriodDto> CreatePeriod(CreatePeriodDto dto);
        Task UpdatePeriod(int id, UpdatePeriodDto dto);
        Task DeletePeriod(int id);
    }
}