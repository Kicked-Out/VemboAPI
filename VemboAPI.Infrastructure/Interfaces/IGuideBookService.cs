using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IGuideBookService
    {
        List<GuideBookDto> GetAll();
        GuideBookDto GetById(int id);
        GuideBookDto Create(CreateGuideBookDto dto);
        void Update(int id, UpdateGuideBookDto dto);
        void Delete(int id);
    }
}
