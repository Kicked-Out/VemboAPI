using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class GuideBookService : IGuideBookService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public GuideBookService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GuideBookDto> GetAll()
        {
            var list = _context.GuideBooks.ToList();
            return _mapper.Map<List<GuideBookDto>>(list);
        }

        public GuideBookDto GetById(int id)
        {
            var item = _context.GuideBooks.Find(id);
            if (item == null)
                throw new KeyNotFoundException("GuideBook not found.");
            return _mapper.Map<GuideBookDto>(item);
        }

        public GuideBookDto Create(CreateGuideBookDto dto)
        {
            var entity = _mapper.Map<GuideBook>(dto);
            _context.GuideBooks.Add(entity);
            _context.SaveChanges();
            return _mapper.Map<GuideBookDto>(entity);
        }

        public void Update(int id, UpdateGuideBookDto dto)
        {
            var entity = _context.GuideBooks.Find(id);
            if (entity == null)
                throw new KeyNotFoundException("GuideBook not found.");

            _mapper.Map(dto, entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.GuideBooks.Find(id);
            if (entity == null)
                throw new KeyNotFoundException("GuideBook not found.");

            _context.GuideBooks.Remove(entity);
            _context.SaveChanges();
        }
    }
}
