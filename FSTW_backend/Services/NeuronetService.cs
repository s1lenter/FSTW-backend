using System.Text;
using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Repositories;

namespace FSTW_backend.Services
{
    public class NeuronetService : INeuronetService
    {
        private INeuronetRepository _neuroRepository;
        private IResumeEditorRepository _resumeEditorRepository;

        public NeuronetService(AppDbContext context)
        {
            _neuroRepository = new NeuronetRepository(context);
            _resumeEditorRepository = new ResumeEditorRepository(context);
        }
        public async Task<ResponseResult<string>> GetAnswer(OnlyResumeInfoDto resumeInfoDto, string question, HttpClient client)
        {
            var aiService = new GptService(client);
            var prevMessages = await _neuroRepository.GetPrevMessages();
            var context = new List<Dictionary<string, string>>();

            foreach (var message in prevMessages)
            {
                context.Add(new Dictionary<string, string>()
                {
                    ["role"] = "user",
                    ["content"] = $"{message.Message}"
                });
                context.Add(new Dictionary<string, string>()
                {
                    ["role"] = "assistant",
                    ["content"] = $"{message.Answer}"
                });
            }

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "user",
                ["content"] = $"{question}"
            });

            var promtInfo = new StringBuilder();
            promtInfo.AppendLine(
                "ты должен помогать пользователю(начинающему it-специалисту студенту) с созданием резюме и его редактированием для поиска первой стажировки-работы.");
            promtInfo.AppendLine(" у резюме строгая структура, так как используется конструктор.");
            promtInfo.AppendLine(
                "структура такая: информация о себе, хобби, навыки, проекты, опыт, достижения, образование и личная информация");
            promtInfo.AppendLine("вот те данные, которые пользователь уже написал:");
            promtInfo.AppendLine($"опыт: {resumeInfoDto.Experience}");
            promtInfo.AppendLine($"о себе: {resumeInfoDto.About}");
            promtInfo.AppendLine($"хобби: {resumeInfoDto.Hobbies}");
            promtInfo.AppendLine($"навыки: {resumeInfoDto.Skills}");


            for (int i = 0; i < resumeInfoDto.Projects.Count; i++)
            {
                promtInfo.AppendLine($"Проект {i + 1}");
                promtInfo.AppendLine(resumeInfoDto.Projects[i].Description);
            }

            for (int i = 0; i < resumeInfoDto.Educations.Count; i++)
            {
                promtInfo.AppendLine($"Образование {i + 1}");
                promtInfo.AppendLine(resumeInfoDto.Educations[i].Level);
                promtInfo.AppendLine(resumeInfoDto.Educations[i].Place);
                promtInfo.AppendLine(resumeInfoDto.Educations[i].Specialization);
                promtInfo.AppendLine($"{resumeInfoDto.Educations[i].StartYear} - {resumeInfoDto.Educations[i].EndYear}");
            }

            for (int i = 0; i < resumeInfoDto.Projects.Count; i++)
            {
                promtInfo.AppendLine($"Достижение {i + 1}");
                promtInfo.AppendLine(resumeInfoDto.Achievements[i].Description);
            }

            promtInfo.AppendLine("ты должен помочь с описанием проектов, достижениями, с блоками информации о себе, задать наводящие вопросы и так далее");
            promtInfo.AppendLine("не повторяй то, что у пользователя хорошая структура резюме, помогай ему с тем вопросом, который он тебе пишет");

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "system",
                ["content"] = promtInfo.ToString()
            });

            var response = await aiService.SendRequest(question, context);
            await _neuroRepository.AddMessage(question, response);
            return ResponseResult<string>.Success(response);
        }
    }
}
