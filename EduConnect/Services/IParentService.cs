﻿using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Services
{
    public interface IParentService
    {
        Task<List<StudentInfo>> GetStudentInfoAsync(string email);
        Task<bool> UpdateProfileAsync(string email, UpdateParentProfile dto, string? imagePath);
        Task<ParentProfile> GetProfileAsync(string email);
    }
}
