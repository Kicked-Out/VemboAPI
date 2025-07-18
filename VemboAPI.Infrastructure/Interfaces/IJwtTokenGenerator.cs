using System;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}

