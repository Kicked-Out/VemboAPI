using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IAdminPanelService
    {
        AdminMenuDto BuildMenu();
    }
}
