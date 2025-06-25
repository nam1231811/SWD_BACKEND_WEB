using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task CreateSubjectAsync(SubjectCreated dto)
        {
            var sub = new Subject
            {
                SubjectId = Guid.NewGuid().ToString(),
                SubjectName = dto.SubjectName,
                TermId = dto.TermId,
            };
            await _subjectRepository.CreateSubjectAsync(sub);
        }

        public async Task DeleteSubjectAsync(string SubjectId)
        {
            await _subjectRepository.DeleteSubjectAsync(SubjectId);
        }

        public async Task<SubjectCreated> GetByIdAsync(string SubjectId)
        {
            var sub = await _subjectRepository.GetByIdAsync(SubjectId);
            if (sub == null) 
            {
                return null;
            }
            return new SubjectCreated
            {
                SubjectId = sub.SubjectId,
                SubjectName = sub.SubjectName,
                TermId = sub.TermId,
            };
        }

        public async Task UpdateSubjectAsync(SubjectCreated dto)
        {
            var sub = await _subjectRepository.GetByIdAsync(dto.SubjectId);
            if(sub == null)
            {
                return;
            }
            sub.SubjectName = dto.SubjectName;
            sub.TermId = dto.TermId;
            await _subjectRepository.UpdateSubjectAsync(sub);
        }
    }
}
