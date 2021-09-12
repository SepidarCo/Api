using Microsoft.AspNetCore.Mvc;
using Sepidar.Business.Interfaces;
using Sepidar.Api.DataAccess.Models.Module;

namespace Sepidar.Service.Controllers.Admin
{
    public class SettingController: AdminController
    {
        private ISettingBusiness settingBusiness;

        public SettingController(ISettingBusiness settingBusiness)
        {
            this.settingBusiness = settingBusiness;
        }

        [HttpGet]
        public dynamic List()
        {
            var list = settingBusiness.List();
            return list;
        }

        [HttpPost]
        public dynamic Create([FromBody]Setting setting)
        {
            settingBusiness.Create(setting);
            return Ok("created");
        }

        [HttpPost]
        public dynamic Update([FromBody] Setting setting)
        {
            settingBusiness.Update(setting);
            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var user = settingBusiness.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] long id)
        {
            settingBusiness.Delete(id);
            return Ok("Deleted");
        }
    }
}
