using System;
using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserPeriodProgressService
    {
        List<UserPeriodProgressDto> GetAllUserPeriodProgress();
        UserPeriodProgressDto GetUserPeriodProgressById(int id);
        UserPeriodProgressDto CreateUserPeriodProgress(CreateUserPeriodProgressDto dto);
        void UpdateUserPeriodProgress(int id, UpdateUserPeriodProgressDto dto);

        void DeleteUserPeriodProgress(int id);
    }
}
