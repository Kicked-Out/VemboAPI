using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class PeriodService : IPeriodService
    {
        private readonly VemboDbContext _dbContext;

        public PeriodService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PeriodDto> GetAllPeriods()
        {
            return _dbContext.Periods
                .Include(p => p.Topics)
                .Select(p => new PeriodDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    TopicsCount = p.Topics.Count
                })
                .ToList();
        }

        public PeriodDto GetPeriodById(int id)
        {
            var period = _dbContext.Periods
                .Include(p => p.Topics)
                .FirstOrDefault(p => p.Id == id);

            if (period == null)
            {
                throw new KeyNotFoundException($"Period with ID {id} not found.");
            }

            return new PeriodDto
            {
                Id = period.Id,
                Title = period.Title,
                Description = period.Description,
                ImageUrl = period.ImageUrl,
                TopicsCount = period.Topics.Count
            };
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
            {
                throw new KeyNotFoundException($"Period with ID {id} not found.");
            }

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
            {
                throw new KeyNotFoundException($"Period with ID {id} not found.");
            }

            _dbContext.Periods.Remove(period);
            _dbContext.SaveChanges();
        }
    }
}
