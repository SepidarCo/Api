using Sepidar.Api.DataAccess.Models.Security;
using Sepidar.BlobManagement;
using Sepidar.Framework;

namespace Sepidar.Business.Interfaces
{
    public interface IUserBusiness
    {
        UserData Login(string userName, string password);

        UserData SignIn(string email, string password);

        UserData Register(string email, string password, string rePassword);

        ListResult<User> List(ListOptions listOptions);

        void Create(Blob pictureBlob, string fullName, string userName, string password, string mobileNumber, string email);

        void Update(long id, Blob pictureBlob, string fullName, string userName, string password, string mobileNumber, string email);

        void Delete(long id);

        dynamic Get(long id);
    }
}
