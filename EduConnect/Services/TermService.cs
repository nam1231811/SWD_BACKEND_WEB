﻿using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class TermService : ITermService
    {
        private readonly ITermRepository _termRepository;

        public TermService(ITermRepository termRepository)
        {
            _termRepository = termRepository;
        }

        public async Task<string> CreateTerm(TermCreated term)
        {
            var terms = new Term
            {
                TermID = Guid.NewGuid().ToString(),
                StartTime = term.StartTime,
                EndTime = term.EndTime,
                CreatedAt = DateTime.Now,
                Mode = term.Mode // thêm Mode khi tạo
            };
            await _termRepository.CreateTerm(terms);
            return terms.TermID;
        }

        public async Task<TermCreated?> GetTermById(string termId)
        {
            var terms = await _termRepository.GetTermById(termId);
            if (terms == null)
            {
                return null;
            }
            return new TermCreated
            {
                TermID = terms.TermID,
                StartTime = terms.StartTime,
                EndTime = terms.EndTime,
                CreatedAt = terms.CreatedAt,
                Mode = terms.Mode
            };
        }

        public async Task UpdateTerm(TermCreated term)
        {
            var terms = await _termRepository.GetTermById(term.TermID);
            if (terms == null)
            {
                return;
            }
            terms.StartTime = term.StartTime;
            terms.EndTime = term.EndTime;
            terms.Mode = term.Mode;
            await _termRepository.UpdateTerm(terms);
        }
    }
}
