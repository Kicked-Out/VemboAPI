using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IPartService
    {
        List<Part> GetAllParts();
        Part GetPartById(int id);
        void CreatePart(string title, int topicId);
        void UpdatePart(int id, string title, int topicId);
        void DeletePart(int id);
    }
}
