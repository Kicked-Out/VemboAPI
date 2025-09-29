using AutoMapper;
using System;

using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Користувач
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.IsBlocked,
                    opt => opt.MapFrom(src => src.LockoutEnd.HasValue && src.LockoutEnd.Value.UtcDateTime > DateTime.UtcNow))
                .ForMember(dest => dest.LockedUntil, opt => opt.MapFrom(src => src.LockoutEnd))
                .ReverseMap()
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockedUntil))
                .ForMember(dest => dest.LockoutEnabled,
                    opt => opt.MapFrom(src => src.LockedUntil.HasValue || src.IsBlocked))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();


            // Прогрес користувача
            CreateMap<UserPeriodProgress, UserPeriodProgressDto>().ReverseMap();
            CreateMap<UserPeriodProgress, CreateUserPeriodProgressDto>().ReverseMap();
            CreateMap<UserPeriodProgress, UpdateUserPeriodProgressDto>().ReverseMap();

            CreateMap<UserTopicProgress, UserTopicProgressDto>().ReverseMap();
            CreateMap<UserTopicProgress, CreateUserTopicProgressDto>().ReverseMap();
            CreateMap<UserTopicProgress, UpdateUserTopicProgressDto>().ReverseMap();

            CreateMap<UserUnitProgress, UserUnitProgressDto>().ReverseMap();
            CreateMap<UserUnitProgress, CreateUserUnitProgressDto>().ReverseMap();
            CreateMap<UserUnitProgress, UpdateUserUnitProgressDto>().ReverseMap();

            CreateMap<UserLevelProgress, UserLevelProgressDto>().ReverseMap();
            CreateMap<UserLevelProgress, CreateUserLevelProgressDto>().ReverseMap();
            CreateMap<UserLevelProgress, UpdateUserLevelProgressDto>().ReverseMap();

            CreateMap<UserLessonProgress, UserLessonProgressDto>().ReverseMap();
            CreateMap<UserLessonProgress, CreateUserLessonProgressDto>().ReverseMap();
            CreateMap<UserLessonProgress, UpdateUserLessonProgressDto>().ReverseMap();

            CreateMap<UserExerciseMistake, UserExerciseMistakeDto>().ReverseMap();
            CreateMap<UserExerciseMistake, CreateUserExerciseMistakeDto>().ReverseMap();
            CreateMap<UserExerciseMistake, UpdateUserExerciseMistakeDto>().ReverseMap();

            // Topic
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Topic, TopicCreateDto>().ReverseMap();
            CreateMap<Topic, TopicUpdateDto>().ReverseMap();

            // Unit
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<Unit, CreateUnitDto>().ReverseMap();
            CreateMap<Unit, UpdateUnitDto>().ReverseMap();

            // Lesson
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();

            // Level
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<Level, CreateLevelDto>().ReverseMap();
            CreateMap<Level, UpdateLevelDto>().ReverseMap();

            // Period
            CreateMap<Period, PeriodDto>().ReverseMap();
            CreateMap<Period, CreatePeriodDto>().ReverseMap();
            CreateMap<Period, UpdatePeriodDto>().ReverseMap();

            // LevelType
            CreateMap<LevelType, LevelTypeDto>().ReverseMap();
            CreateMap<LevelType, CreateLevelTypeDto>().ReverseMap();
            CreateMap<LevelType, UpdateLevelTypeDto>().ReverseMap();

            // Exercise
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<Exercise, CreateExerciseDto>().ReverseMap();
            CreateMap<Exercise, UpdateExerciseDto>().ReverseMap();

            // ExerciseType
            CreateMap<ExerciseType, ExerciseTypeDto>().ReverseMap();
            CreateMap<ExerciseType, CreateExerciseTypeDto>().ReverseMap();
            CreateMap<ExerciseType, UpdateExerciseTypeDto>().ReverseMap();

            // Question
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, CreateQuestionDto>().ReverseMap();
            CreateMap<Question, UpdateQuestionDto>().ReverseMap();

            // Answer
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, CreateAnswerDto>().ReverseMap();
            CreateMap<Answer, UpdateAnswerDto>().ReverseMap();

            // GuideBook
            CreateMap<GuideBook, GuideBookDto>().ReverseMap();
            CreateMap<GuideBook, CreateGuideBookDto>().ReverseMap();
            CreateMap<GuideBook, UpdateGuideBookDto>().ReverseMap();

            CreateMap<Achievement, AchievementDto>().ReverseMap();
            CreateMap<CreateAchievementDto, Achievement>().ReverseMap();
            CreateMap<UpdateAchievementDto, Achievement>().ReverseMap();

            CreateMap<AchievementLevel, AchievementLevelDto>().ReverseMap();
            CreateMap<CreateAchievementLevelDto, AchievementLevel>().ReverseMap();
            CreateMap<UpdateAchievementLevelDto, AchievementLevel>().ReverseMap();

            CreateMap<UserAchievement, UserAchievementDto>().ReverseMap();
            CreateMap<CreateUserAchievementDto, UserAchievement>().ReverseMap();
            CreateMap<UpdateUserAchievementDto, UserAchievement>().ReverseMap();

            CreateMap<QuestDefinition, QuestDefinitionDto>().ReverseMap();
            CreateMap<CreateQuestDefinitionDto, QuestDefinition>();
            CreateMap<UpdateQuestDefinitionDto, QuestDefinition>();

            CreateMap<Quest, QuestDto>().ReverseMap();
            CreateMap<CreateQuestDto, Quest>();
            CreateMap<UpdateQuestDto, Quest>();

            CreateMap<QuestType, QuestTypeDto>().ReverseMap();
            CreateMap<CreateQuestTypeDto, QuestType>();
            CreateMap<UpdateQuestTypeDto, QuestType>();

            CreateMap<UserQuestProgress, UserQuestProgressDto>().ReverseMap();
            CreateMap<CreateUserQuestProgressDto, UserQuestProgress>();
            CreateMap<UpdateUserQuestProgressDto, UserQuestProgress>();

            CreateMap<Medal, MedalDto>();
            CreateMap<CreateMedalDto, Medal>();
            CreateMap<UpdateMedalDto, Medal>();

            CreateMap<UserMedal, UserMedalDto>();
            CreateMap<CreateUserMedalDto, UserMedal>();
            CreateMap<UpdateUserMedalDto, UserMedal>();

            // Badge
            CreateMap<Badge, BadgeDto>().ReverseMap();
            CreateMap<CreateBadgeDto, Badge>();
            CreateMap<UpdateBadgeDto, Badge>();

            CreateMap<Badge, BadgeDto>();
            CreateMap<CreateBadgeDto, Badge>();
            CreateMap<UpdateBadgeDto, Badge>();

            CreateMap<UserBadge, UserBadgeDto>();
            CreateMap<CreateUserBadgeDto, UserBadge>();
            CreateMap<UpdateUserBadgeDto, UserBadge>();

            CreateMap<UserStreak, UserStreakDto>().ReverseMap();
            CreateMap<UserStreak, CreateUserStreakDto>().ReverseMap();
            CreateMap<UserStreak, UpdateUserStreakDto>().ReverseMap();
            CreateMap<UserStreakDay, UserStreakDayDto>().ReverseMap();
            CreateMap<UserStreakDay, CreateUserStreakDayDto>().ReverseMap();
            CreateMap<UserStreakDay, UpdateUserStreakDayDto>().ReverseMap();

            CreateMap<UserStatistic, UserStatisticDto>().ReverseMap();
            CreateMap<CreateUserStatisticDto, UserStatistic>().ReverseMap();
            CreateMap<UpdateUserStatisticDto, UserStatistic>().ReverseMap();

            CreateMap<UserLeaderBoardEntry, UserLeaderBoardEntryDto>().ReverseMap();
            CreateMap<CreateUserLeaderBoardEntryDto, UserLeaderBoardEntry>().ReverseMap();
            CreateMap<UpdateUserLeaderBoardEntryDto, UserLeaderBoardEntry>().ReverseMap();
        }
    }
}
