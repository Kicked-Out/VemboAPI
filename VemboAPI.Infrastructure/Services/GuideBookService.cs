using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class GuideBookService : IGuideBookService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public GuideBookService(
            VemboDbContext context,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService ver)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _ver = ver;
        }

        public List<GuideBookDto> GetAll()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:guidebooks:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var items = _context.GuideBooks.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<GuideBookDto>>(items);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public GuideBookDto GetById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:guidebook:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var item = _context.GuideBooks.Find(id);
                if (item == null)
                    throw new KeyNotFoundException("GuideBook not found.");

                var mapped = _mapper.Map<GuideBookDto>(item);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public GuideBookDto Create(CreateGuideBookDto dto)
        {
            var entity = _mapper.Map<GuideBook>(dto);
            _context.GuideBooks.Add(entity);
            _context.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація через нову версію

            return _mapper.Map<GuideBookDto>(entity);
        }

        public void Update(int id, UpdateGuideBookDto dto)
        {
            var entity = _context.GuideBooks.Find(id);
            if (entity == null)
                throw new KeyNotFoundException("GuideBook not found.");

            _mapper.Map(dto, entity);
            _context.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void Delete(int id)
        {
            var entity = _context.GuideBooks.Find(id);
            if (entity == null)
                throw new KeyNotFoundException("GuideBook not found.");

            _context.GuideBooks.Remove(entity);
            _context.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
