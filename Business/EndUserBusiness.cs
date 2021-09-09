using Sepidar.Api.DataAccess.Models.Security;
using Sepidar.Business.Interfaces;
using Sepidar.EntityFramework;

namespace Sepidar.Business
{
    public class EndUserBusiness : IEndUserBusiness
    {
        private Repository<EndUser> endUserRepository;

        public EndUserBusiness(Repository<EndUser> endUserRepository)
        {
            this.endUserRepository = endUserRepository;
        }

        public void Create(long id)
        {
            var endUser = new EndUser
            {
                Id = id
            };

            endUserRepository.Create(endUser);
        }

        public void Delete(long id)
        {
            endUserRepository.Delete(id);
        }
    }
}
