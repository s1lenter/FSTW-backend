using FSTW_backend.Dto;
using FSTW_backend.Models;

namespace FSTW_backend
{
    public static class InternshipCategory
    {
        private static Dictionary<string, List<string>> categories = new Dictionary<string, List<string>>()
            {
                { "Архитектура", new List<string>() },
                { "Финансы и банковское дело", new List<string>() },
                { "IT и разработка", new List<string>()
                    {
                        "DevOps-инженер",
                        "Программист, разработчик",
                        "Руководитель группы разработки",
                        "Сетевой инженер",
                        "Специалист по информационной безопасности",
                        "Директор по информационным технологиям (CIO)",
                        "Технический директор (CTO)",
                        "Специалист технической поддержки",
                        "Тестирование",
                        "Data Science",
                        "Дата-сайентист",
                        "Кибербезопасность",
                        "Техническая безопасность",
                        "Backend-разработка",
                        "Data Engineering",
                        "Frontend-разработка",
                        "Fullstack-разработка",
                        "DevOps"
                    }
                },
                { "Маркетинг и дизайн", new List<string>()
                    {
                        "Арт-директор, креативный директор",
                        "Гейм-дизайнер",
                        "Дизайнер, художник",
                        "UX-редактор"
                    }
                },
                { "Инженерия и производство", new List<string>() },
                { "Управление и консалтинг", new List<string>()
                    {
                        "Менеджер продукта",
                        "Руководитель проектов",
                        "Бизнес-аналитик",
                        "Проектный менеджмент"
                    }
                },
                { "Технические и сервисные специальности", new List<string>()
                    {
                        "Специалист технической поддержки",
                        "Администрирование баз данных"
                    }
                },
                { "Аналитика", new List<string>()
                    {
                        "BI-аналитик, аналитик данных",
                        "Аналитик",
                        "Бизнес-аналитик",
                        "Продуктовый аналитик",
                        "Руководитель отдела аналитики",
                        "Методолог",
                        "Системная аналитика",
                        "Аналитика данных",
                    }
                }
            };
        public static Dictionary<string, List<string>> Categories { get; private set; }

        public static string? FindCategory(string internshipDirection)
        {
            foreach (var key in categories.Keys)
            {
                foreach (var dir in categories[key])
                    if (internshipDirection == dir)
                        return key;
            }
            return null;
        }
    }
}
