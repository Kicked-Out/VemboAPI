using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VemboAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Користувач
            CreateMap<User, UserDto>().ReverseMap();

            // Прогрес користувача
            CreateMap<UserPeriodProgress, UserPeriodProgressDto>().ReverseMap();
            CreateMap<UserTopicProgress, UserTopicProgressDto>().ReverseMap();
            CreateMap<UserUnitProgress, UserUnitProgressDto>().ReverseMap();
            CreateMap<UserLevelProgress, UserLevelProgressDto>().ReverseMap();
            CreateMap<UserLessonProgress, UserLessonProgressDto>().ReverseMap();
            CreateMap<UserExerciseMistake, UserExerciseMistakeDto>().ReverseMap();

            // Основні сутності
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Topic, TopicCreateDto>().ReverseMap(); // якщо потрібно
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<Period, PeriodDto>().ReverseMap();

            // Вправи
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<ExerciseType, ExerciseTypeDto>().ReverseMap();

            // Питання/Відповіді
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
        }
    }
}
