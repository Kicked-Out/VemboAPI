using System;
using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserPeriodProgressService
    {
        List<UserPeriodProgressDto> GetAllUserPeriodProgress();
        UserPeriodProgressDto GetUserPeriodProgressById(int id);
        UserPeriodProgressDto CreateUserPeriodProgress(int userId, int periodId, bool isCompleted);
        void UpdateUserPeriodProgress(int id, int userId, int periodId, bool isCompleted);
        void DeleteUserPeriodProgress(int id);
    }
}
