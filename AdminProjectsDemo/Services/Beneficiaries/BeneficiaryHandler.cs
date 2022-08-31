using AdminProjectsDemo.DataContext;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Base;

namespace AdminProjectsDemo.Services.Beneficiaries
{
    public class BeneficiaryHandler : BaseService<Beneficiario>, IBeneficiaryHandler
    {
        public BeneficiaryHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext) 
        { }
    }
}
