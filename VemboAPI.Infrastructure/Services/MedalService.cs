using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class MedalService : IMedalService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public MedalService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MedalDto>> GetAllAsync()
        {
            var items = await _context.Medals.ToListAsync();
            return _mapper.Map<List<MedalDto>>(items);
        }

        public async Task<MedalDto> GetByIdAsync(int id)
        {
            var entity = await _context.Medals.FindAsync(id) ?? throw new KeyNotFoundException($"Medal with ID {id} not found.");
            return _mapper.Map<MedalDto>(entity);
        }

        public async Task<MedalDto> CreateAsync(CreateMedalDto dto)
        {
            var entity = _mapper.Map<Medal>(dto);
            _context.Medals.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MedalDto>(entity);
        }

        public async Task<MedalDto> UpdateAsync(int id, UpdateMedalDto dto)
        {
            var entity = await _context.Medals.FindAsync(id) ?? throw new KeyNotFoundException($"Medal with ID {id} not found.");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<MedalDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Medals.FindAsync(id) ?? throw new KeyNotFoundException($"Medal with ID {id} not found.");
            _context.Medals.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
