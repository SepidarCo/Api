using Sepidar.Api.DataAccess.Models;
using Sepidar.EntityFramework;
using System.Linq;
using Sepidar.Framework.Extensions;
using Sepidar.Framework;
using Sepidar.Api.DataAccess.Models.Module;
using Sepidar.Business.Interfaces;

namespace Sepidar.Business
{
    public class CalendarBusiness : ICalendarBusiness
    {
        private Repository<Calendar> calendarRepository;

        public CalendarBusiness(Repository<Calendar> calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }

        public void Create(Calendar calendar)
        {
            calendarRepository.Create(calendar);
        }

        public void Delete(long id)
        {
            calendarRepository.Delete(id);
        }

        public dynamic Get(long id)
        {
            var calendar = calendarRepository.Get(id);
            return calendar;
        }

        public dynamic List(long languageId, ListOptions listOptions)
        {
            var list = calendarRepository.All
                .Where(i => i.LanguageId == languageId)
                .OrderByDescending(i => i.OrderNumber > 0)
                .ThenBy(i => i.OrderNumber).ApplyListOptionsAndGetTotalCount(listOptions);

            return list;
        }

        public void Update(Calendar calendar)
        {
            var model = calendarRepository.Get(calendar.Id);

            model.Title = calendar.Title;
            model.ShortDate = calendar.ShortDate;
            model.Date = calendar.Date;
            model.Address = calendar.Address;
            model.IsActive = calendar.IsActive;
            model.OrderNumber = calendar.OrderNumber;

            calendarRepository.Update(model);
        }

        public dynamic CalendarList(long languageId)
        {
            var list = calendarRepository.All
                .Where(i => i.IsActive != false && i.LanguageId == languageId)
                .OrderByDescending(i => i.OrderNumber > 0).ThenBy(i => i.OrderNumber).ToList();
            return list;
        }
    }
}
