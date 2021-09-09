using Sepidar.Framework;
using Sepidar.Api.DataAccess.Models;
using Sepidar.Api.DataAccess.Models.Module;

namespace Sepidar.Business.Interfaces
{
    public interface ICalendarBusiness
    {
        dynamic List(long languageId, ListOptions listOptions);

        dynamic CalendarList(long languageId);

        void Create(Calendar calendar);

        void Update(Calendar calendar);

        dynamic Get(long id);

        void Delete(long id);
    }
}
