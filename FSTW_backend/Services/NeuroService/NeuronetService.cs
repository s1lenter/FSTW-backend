using System.Text;
using FSTW_backend.Dto;
using FSTW_backend.Dto.ResumeDto;
using FSTW_backend.Models;
using FSTW_backend.Repositories;
using FSTW_backend.Repositories.Neuro;

namespace FSTW_backend.Services.Neuro
{
    public class NeuronetService : INeuronetService
    {
        private INeuronetRepository _neuroRepository;

        public NeuronetService(AppDbContext context)
        {
            _neuroRepository = new NeuronetRepository(context);
        }
        public async Task<ResponseResult<string>> GetResumeAnswer(int userId, int resumeId, OnlyResumeInfoDto resumeInfoDto, string question, HttpClient client)
        {
            var aiService = new GptService(client);
            var prevMessages = await _neuroRepository.GetResumePrevMessages(userId, resumeId);
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

            var promtInfo = new StringBuilder();
            promtInfo.AppendLine(
                "Ты должен помогать пользователю(начинающему it-специалисту студенту) с созданием резюме и его редактированием для поиска первой стажировки или работы.");
            promtInfo.AppendLine("Ты должен отвечать только на русском языке.");
            promtInfo.AppendLine("У резюме строгая структура, так как используется конструктор.");
            promtInfo.AppendLine(
                "Структура такая: информация о себе, хобби, навыки, проекты, опыт, достижения, образование и личная информация.");
            promtInfo.AppendLine("вот те данные, которые пользователь уже написал:");
            promtInfo.AppendLine("личные данные тебе не отправляются, потому что их не нужно никак редактировать");
            promtInfo.AppendLine($"Опыт: {resumeInfoDto.Experience}");
            promtInfo.AppendLine($"О себе: {resumeInfoDto.About}");
            promtInfo.AppendLine($"Хобби: {resumeInfoDto.Hobbies}");
            promtInfo.AppendLine($"Навыки: {resumeInfoDto.Skills}");


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

            promtInfo.AppendLine("Ты должен помочь с описанием проектов, достижениями, с блоками информации о себе, задать наводящие вопросы, дать подсказки по улучшению на основе того, что хотят видеть работодаели и hr-специалисты.");
            promtInfo.AppendLine("Обязательно обращай внимение на то, что именно тебя спрашивает или просит пользователь, помогай ему с его вопросом");
            promtInfo.AppendLine("Если тебя спрашивают о чем-то не связанном с резюме, it и работой, то говори что на такие вопросы ты не отвечаешь");
            promtInfo.AppendLine("");

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "system",
                ["content"] = promtInfo.ToString()
            });

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "user",
                ["content"] = $"{question}"
            });

            var response = await aiService.SendRequest(question, context);

            if (response is null)
                response = "Сервер не отвечает, попробуйте позже";
            else
                await _neuroRepository.AddResumeMessage(userId, resumeId, question, response);
            return ResponseResult<string>.Success(response);
        }

        public async Task<ResponseResult<string>> GetDefaultAnswer(int userId, string question, HttpClient client)
        {
            var aiService = new GptService(client);
            var prevMessages = await _neuroRepository.GetDefaultPrevMessages(userId);
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

            var promtInfo = new StringBuilder();
            promtInfo.AppendLine(
                "Ты должен помогать пользователю(начинающему it-специалисту студенту) с вопросам касательно первой работы, стажировки, как попасть в it и что для этого делать, что изучать.");
            promtInfo.AppendLine("Ты должен отвечать только на русском языке.");
            promtInfo.AppendLine(
                "Если тебя спрашивают что можно изучить, то старайся сразу давать ресурсы, ссылки, курсы, книги, чтобы пользователю было легче начать обучение.");
            promtInfo.AppendLine(
                "Также ты должен помогать для подготовки к собеседованиям, предоставить возможность проверки зананий пользователя в той сфере, на которую он ориентируется.");
            promtInfo.AppendLine("Если тебя спрашивают о чем-то не связанном с it и работой, то говори что на такие вопросы ты не отвечаешь.");

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "system",
                ["content"] = promtInfo.ToString()
            });

            context.Add(new Dictionary<string, string>()
            {
                ["role"] = "user",
                ["content"] = $"{question}"
            });

            var response = await aiService.SendRequest(question, context);

            if (response is null)
                response = "Сервер не отвечает, попробуйте позже";
            else
                await _neuroRepository.AddDefaultMessage(question, response, userId);

            return ResponseResult<string>.Success(response);
        }

        public async Task<ResponseResult<List<NeuronetDto>>> GetDefaultChatHistory(int userId, int count, int page)
        {
            var history = await _neuroRepository.GetMessagesDefaultHistory(userId, count, page);
            return ResponseResult<List<NeuronetDto>>.Success(history);
        }

        public async Task<ResponseResult<List<NeuronetDto>>> GetResumeChatHistory(int userId, int count, int page)
        {
            var history = await _neuroRepository.GetMessagesResumeHistory(userId, count, page);
            return ResponseResult<List<NeuronetDto>>.Success(history);
        }

        public async Task FillDb(string message, int count, int userid)
        {
            await _neuroRepository.FillDb(message, count, userid);
        }

    }
}
