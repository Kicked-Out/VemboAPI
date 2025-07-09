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

        public void CreatePeriod(string title, string description, string imageUrl)
        {
            var period = new Period
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl
            };

            _dbContext.Periods.Add(period);
            _dbContext.SaveChanges();
        }

        public void UpdatePeriod(int id, string title, string description, string imageUrl)
        {
            var period = _dbContext.Periods.Find(id);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            period.Title = title;
            period.Description = description;
            period.ImageUrl = imageUrl;

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
