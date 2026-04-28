using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface ICompaniesService
    {
        public List<ClsCompanies> GetAllCompanies();

        public ClsCompanies GetCompanyById(int id);

        public ClsCompanies GetCompanyByUserCompany(string userCompany);
        public void AddCompany(ClsCompanies company);

        public bool EditCompany(ClsCompanies company);

        public bool DeleteCompany(ClsCompanies company);
    }
}