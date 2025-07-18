﻿using FSTW_backend.Dto;
using FSTW_backend.Dto.ResumeDto;

namespace FSTW_backend.Services.Neuro
{
    public interface INeuronetService
    {
        public Task<ResponseResult<string>> GetResumeAnswer(int userId, int resumeId, OnlyResumeInfoDto resumeInfoDto, string question, HttpClient client);

        public Task<ResponseResult<string>> GetDefaultAnswer(int userId, string question, HttpClient client);

        public Task<ResponseResult<List<NeuronetDto>>> GetDefaultChatHistory(int userId, int count, int page);

        public Task<ResponseResult<List<NeuronetDto>>> GetResumeChatHistory(int userId, int resumeId, int count, int page);

        public Task FillDb(string message, int count, int userId);

        public Task<string> SendRequests(string message, HttpClient client);
    }
}
