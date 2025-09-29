using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        public AdminMenuDto BuildMenu()
        {
            var sections = new List<AdminMenuSectionDto>
            {
                new AdminMenuSectionDto
                {
                    Title = "Контент",
                    Icon = "content",
                    Items = new List<AdminMenuItemDto>
                    {
                        new AdminMenuItemDto
                        {
                            Title = "Створити таску (Quest)",
                            Description = "Розробка нової таски (Quest) для прогресу користувачів.",
                            HttpMethod = "POST",
                            Endpoint = "/api/quests",
                            RelatedEntities = new []{"Quest", "QuestDefinition"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Створити вправу",
                            Description = "Створення вправи (Exercise) з повним налаштуванням контенту та медіа.",
                            HttpMethod = "POST",
                            Endpoint = "/api/Exercise",
                            RelatedEntities = new []{"Exercise", "Lesson", "Unit"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Створити рівень",
                            Description = "Додавання нового рівня до обраного юніта.",
                            HttpMethod = "POST",
                            Endpoint = "/api/Level",
                            RelatedEntities = new []{"Level", "Unit"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Створити урок",
                            Description = "Додавання уроку в межах конкретного рівня.",
                            HttpMethod = "POST",
                            Endpoint = "/api/Lesson",
                            RelatedEntities = new []{"Lesson", "Level"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        }
                    }
                },
                new AdminMenuSectionDto
                {
                    Title = "Питання та відповіді",
                    Icon = "quiz",
                    Items = new List<AdminMenuItemDto>
                    {
                        new AdminMenuItemDto
                        {
                            Title = "Створити питання",
                            Description = "Формування питання для вправи з типом Single або Multi.",
                            HttpMethod = "POST",
                            Endpoint = "/api/Question",
                            RelatedEntities = new []{"Question", "Exercise"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Додати відповіді (Single)",
                            Description = "Створює варіант відповіді з єдиною правильною позначкою (IsCorrect = true лише в одного).",
                            HttpMethod = "POST",
                            Endpoint = "/api/Answer",
                            RelatedEntities = new []{"Answer", "Question"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Додати відповіді (Multi)",
                            Description = "Додає варіанти відповіді типу Multi (кілька записів з IsCorrect = true).",
                            HttpMethod = "POST",
                            Endpoint = "/api/Answer",
                            RelatedEntities = new []{"Answer", "Question"},
                            AllowedRoles = new []{"Admin", "ContentManager"}
                        }
                    }
                },
                new AdminMenuSectionDto
                {
                    Title = "Користувачі",
                    Icon = "users",
                    Items = new List<AdminMenuItemDto>
                    {
                        new AdminMenuItemDto
                        {
                            Title = "Заблокувати користувача",
                            Description = "Блокує доступ користувача до платформи на визначений час.",
                            HttpMethod = "POST",
                            Endpoint = "/api/User/{id}/block",
                            RelatedEntities = new []{"User"},
                            AllowedRoles = new []{"Admin"}
                        },
                        new AdminMenuItemDto
                        {
                            Title = "Розблокувати користувача",
                            Description = "Знімає блокування та повертає доступ користувача.",
                            HttpMethod = "POST",
                            Endpoint = "/api/User/{id}/unblock",
                            RelatedEntities = new []{"User"},
                            AllowedRoles = new []{"Admin"}
                        }
                    }
                }
            };

            return new AdminMenuDto
            {
                Sections = sections
            };
        }
    }
}
