using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Services
{
    public class PartService : IPartService
    {
        private VemboDbContext _vemboDbContext;

        public PartService(VemboDbContext vemboDbContext)
        {
            _vemboDbContext = vemboDbContext;
        }

        public List<Part> GetAllParts()
        {
            return _vemboDbContext.Parts.ToList();
        }

        public Part GetPartById(int id)
        {
            var part = _vemboDbContext.Parts.Find(id);
            if (part == null)
            {
                throw new KeyNotFoundException($"Part with ID {id} not found.");
            }
            return part;
        }

        public void CreatePart(string title, int topicId)
        {
            var topic = _vemboDbContext.Topics.Find(topicId);
            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");
            }

            var part = new Part
            {
                Title = title,
                TopicId = topicId
            };

            _vemboDbContext.Parts.Add(part);
            _vemboDbContext.SaveChanges();
        }

        public void UpdatePart(int id, string title, int topicId)
        {
            var part = _vemboDbContext.Parts.Find(id);
            if (part == null)
            {
                throw new KeyNotFoundException($"Part with ID {id} not found.");
            }

            var topic = _vemboDbContext.Topics.Find(topicId);
            if (topic == null)
            {
                throw new KeyNotFoundException($"Topic with ID {topicId} not found.");
            }

            part.Title = title;
            part.TopicId = topicId;

            _vemboDbContext.Parts.Update(part);
            _vemboDbContext.SaveChanges();
        }

        public void DeletePart(int id)
        {
            var part = _vemboDbContext.Parts.Find(id);
            if (part == null)
            {
                throw new KeyNotFoundException($"Part with ID {id} not found.");
            }

            _vemboDbContext.Parts.Remove(part);
            _vemboDbContext.SaveChanges();
        }
    }
}
