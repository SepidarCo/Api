using Sepidar.Api.DataAccess.Models.Module;

namespace Sepidar.Business.Interfaces
{
    public interface ISettingBusiness
    {
        dynamic List();

        void Create(Setting setting);

        void Update(Setting setting);

        dynamic Get(long id);

        void Delete(long id);
    }
}
