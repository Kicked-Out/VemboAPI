using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public BadgeService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BadgeDto> GetAllBadges()
        {
            var badges = _dbContext.Badges.ToList();
            return _mapper.Map<List<BadgeDto>>(badges);
        }

        public BadgeDto GetBadgeById(int id)
        {
            var badge = _dbContext.Badges.Find(id);
            if (badge == null)
                throw new KeyNotFoundException($"Badge with ID {id} not found.");

            return _mapper.Map<BadgeDto>(badge);
        }

        public BadgeDto CreateBadge(CreateBadgeDto dto)
        {
            var badge = _mapper.Map<Badge>(dto);
            _dbContext.Badges.Add(badge);
            _dbContext.SaveChanges();
            return _mapper.Map<BadgeDto>(badge);
        }

        public void UpdateBadge(int id, UpdateBadgeDto dto)
        {
            var badge = _dbContext.Badges.Find(id);
            if (badge == null)
                throw new KeyNotFoundException($"Badge with ID {id} not found.");

            _mapper.Map(dto, badge);
            _dbContext.SaveChanges();
        }

        public void DeleteBadge(int id)
        {
            var badge = _dbContext.Badges.Find(id);
            if (badge == null)
                throw new KeyNotFoundException($"Badge with ID {id} not found.");

            _dbContext.Badges.Remove(badge);
            _dbContext.SaveChanges();
        }
    }
}
