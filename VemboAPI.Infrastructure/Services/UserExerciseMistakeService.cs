using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserExerciseMistakeService : IUserExerciseMistakeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserExerciseMistakeService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserExerciseMistakeDto>> GetAllMistakes()
        {
            var mistakes = await _dbContext.UserExerciseMistakes.ToListAsync();

            return _mapper.Map<List<UserExerciseMistakeDto>>(mistakes);
        }

        public async Task<UserExerciseMistakeDto> GetMistakeById(int id)
        {
            var mistake = await _dbContext.UserExerciseMistakes.FindAsync(id);

            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            return _mapper.Map<UserExerciseMistakeDto>(mistake);
        }

        public async Task<UserExerciseMistakeDto> CreateMistake(CreateUserExerciseMistakeDto dto)
        {
            var mistake = _mapper.Map<UserExerciseMistake>(dto);

            await _dbContext.UserExerciseMistakes.AddAsync(mistake);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserExerciseMistakeDto>(mistake);
        }


        public async Task UpdateMistake(int id, UpdateUserExerciseMistakeDto dto)
        {
            var mistake = await _dbContext.UserExerciseMistakes.FindAsync(id);

            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            _mapper.Map(dto, mistake);
            
            await _dbContext.SaveChangesAsync();
        }



        public async Task DeleteMistake(int id)
        {
            var mistake = await _dbContext.UserExerciseMistakes.FindAsync(id);

            if (mistake == null)
                throw new KeyNotFoundException($"Mistake with ID {id} not found.");

            _dbContext.UserExerciseMistakes.Remove(mistake);
            await _dbContext.SaveChangesAsync();
        }
    }
}
