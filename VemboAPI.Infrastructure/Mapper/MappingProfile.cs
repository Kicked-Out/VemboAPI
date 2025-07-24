using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

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
            CreateMap<UserExerciseMistake, UserExerciseMistakeDto>().ReverseMap();

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
            CreateMap<Answer, UpdateAnswerDto>().ReverseMap(); // ❗ Перевір: "Unswer" чи "Answer"
        }
    }
}
