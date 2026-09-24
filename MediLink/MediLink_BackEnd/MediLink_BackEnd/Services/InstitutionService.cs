using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Services
{
    public interface IInstitutionService
    {
        public Task<Institution> AddInstiution(Institution institution);
    }
    public class InstitutionService : IInstitutionService
    {
        private readonly MediLinkContext _dbContext;

        public InstitutionService(MediLinkContext context)
        {
            _dbContext = context;
        }

        public async Task<Institution> AddInstiution(Institution institution)
        {
            try
            {
                _dbContext.Institutions.Add(institution);
                await _dbContext.SaveChangesAsync();

                return institution;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
