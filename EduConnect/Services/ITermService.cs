using EduConnect.DTO;

public interface ITermService
{
    Task<TermCreated?> GetTermById(string termId);
    Task<string> CreateTerm(TermCreated term);
    Task UpdateTerm(TermCreated term);
}
