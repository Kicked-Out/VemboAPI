using System.Threading.Tasks;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Jobs
{
    public class CacheWarmupJob
    {
        private readonly IPeriodService _periods;
        private readonly ITopicService _topics;
        private readonly IUnitService _units;
        private readonly ILessonService _lessons;
        private readonly IExerciseTypeService _exerciseTypes;
        private readonly ILevelTypeService _levelTypes;
        private readonly ILevelService _levels;
        private readonly IExerciseService _exercises;
        private readonly IQuestionService _questions;
        private readonly IAnswerService _answers;
        private readonly IGuideBookService _guidebooks;
        private readonly IAchievementService _achievements;
        private readonly IAchievementLevelService _achievementLevels;

        public CacheWarmupJob(
            IPeriodService periods,
            ITopicService topics,
            IUnitService units,
            ILessonService lessons,
            IExerciseTypeService exerciseTypes,
            ILevelTypeService levelTypes,
            ILevelService levels,
            IExerciseService exercises,
            IQuestionService questions,
            IAnswerService answers,
            IGuideBookService guidebooks,
            IAchievementService achievements,
            IAchievementLevelService achievementLevels)
        {
            _periods = periods;
            _topics = topics;
            _units = units;
            _lessons = lessons;
            _exerciseTypes = exerciseTypes;
            _levelTypes = levelTypes;
            _levels = levels;
            _exercises = exercises;
            _questions = questions;
            _answers = answers;
            _guidebooks = guidebooks;
            _achievements = achievements;
            _achievementLevels = achievementLevels;
        }

        // Прогріваємо все кешоване (лише публічний контент)
        public async Task RunAsync()
        {
            // Якщо хочеш — можна паралелити через Task.WhenAll,
            // але послідовно теж ок для стабільності.

            // Базовий контент
            await _periods.GetAllPeriods();
            await _topics.GetAllTopics();
            await _units.GetAllUnits();
            await _lessons.GetAllLessons();

            // Довідники / типи
            await _exerciseTypes.GetAllExerciseTypes();
            await _levelTypes.GetAll();

            // Рівні
            await _levels.GetAllLevels();

            // Вправи, питання, відповіді
            await _exercises.GetAllExercise();
            await _questions.GetAllQuestions();
            await _answers.GetAllAnswers();

            // Гайдбуки
            _guidebooks.GetAll();

            // Ачивменти (асинхронні сервіси)
            await _achievements.GetAllAsync();
            await _achievementLevels.GetAllAsync();
        }
    }
}
