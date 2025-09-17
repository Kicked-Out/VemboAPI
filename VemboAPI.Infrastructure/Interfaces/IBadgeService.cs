using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IBadgeService
    {
        List<BadgeDto> GetAllBadges();
        BadgeDto GetBadgeById(int id);
        BadgeDto CreateBadge(CreateBadgeDto dto);
        void UpdateBadge(int id, UpdateBadgeDto dto);
        void DeleteBadge(int id);
    }
}
