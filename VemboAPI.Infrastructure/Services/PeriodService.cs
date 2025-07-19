using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace VemboAPI.Infrastructure.Services
{
    public class PeriodService : IPeriodService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public PeriodService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<PeriodDto> GetAllPeriods()
        {
            var periods = _dbContext.Periods
                .Include(p => p.Topics)
                .ToList();

            // TopicsCount мапиться вручну, бо не в Entity
            var result = _mapper.Map<List<PeriodDto>>(periods);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].TopicsCount = periods[i].Topics.Count;
            }
            return result;
        }

        public PeriodDto GetPeriodById(int id)
        {
            var period = _dbContext.Periods
                .Include(p => p.Topics)
                .FirstOrDefault(p => p.Id == id);

            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            var dto = _mapper.Map<PeriodDto>(period);
            dto.TopicsCount = period.Topics.Count;
            return dto;
        }

        public PeriodDto CreatePeriod(CreatePeriodDto dto)
        {
            var period = new Period
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl
            };

            _dbContext.Periods.Add(period);
            _dbContext.SaveChanges();

            var result = _mapper.Map<PeriodDto>(period);
            result.TopicsCount = 0; 

            return result;
        }


        public void UpdatePeriod(int id, UpdatePeriodDto dto)
        {
            var period = _dbContext.Periods.Find(id);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            period.Title = dto.Title;
            period.Description = dto.Description;
            period.ImageUrl = dto.ImageUrl;

            _dbContext.Periods.Update(period);
            _dbContext.SaveChanges();
        }


        public void DeletePeriod(int id)
        {
            var period = _dbContext.Periods.Find(id);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            _dbContext.Periods.Remove(period);
            _dbContext.SaveChanges();
        }
    }
}
