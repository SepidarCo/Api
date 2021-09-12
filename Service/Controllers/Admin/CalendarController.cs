using Microsoft.AspNetCore.Mvc;
using Sepidar.Framework;
using Sepidar.Api.DataAccess.Models.Module;
using Sepidar.Business.Interfaces;

namespace Sepidar.Service.Controllers.Admin
{
    public class CalendarController: AdminController
    {
        private ICalendarBusiness calendarBusiness;

        public CalendarController(ICalendarBusiness calendarBusiness)
        {
            this.calendarBusiness = calendarBusiness;
        }

        [HttpGet]
        public dynamic List(long languageId,[ModelBinder] ListOptions listOptions)
        {
            var list = calendarBusiness.List(languageId, listOptions);
            return list;
        }

        [HttpPost]
        public dynamic Create([FromBody]Calendar calendar)
        {
            calendarBusiness.Create(calendar);
            return Ok("created");
        }

        [HttpPost]
        public dynamic Update([FromBody]Calendar calendar)
        {
            calendarBusiness.Update(calendar);
            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var user = calendarBusiness.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] long id)
        {
            calendarBusiness.Delete(id);
            return Ok("Deleted");
        }
    }
}
