using Sepidar.Api.DataAccess.Models.Security;
using Sepidar.Business.Interfaces;
using Sepidar.EntityFramework;

namespace Sepidar.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private Repository<Admin> adminRepository;

        public AdminBusiness(Repository<Admin> adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public void Create(long id)
        {
            var admin = new Admin
            {
                Id = id
            };

            adminRepository.Create(admin);
        }

        public void Delete(long id)
        {
            adminRepository.Delete(id);
        }
    }
}
