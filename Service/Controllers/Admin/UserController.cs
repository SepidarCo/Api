using Sepidar.Framework;
using Sepidar.BlobManagement;
using Sepidar.Framework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sepidar.Business.Interfaces;
using Sepidar.Service.Models;
using System.IO;

namespace Sepidar.Service.Controllers.Admin
{
    public class UserController : AdminController
    {
        private IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpGet]
        public dynamic List([ModelBinder] ListOptions listOptions)
        {
            var list = userBusiness.List(listOptions);
            return list;
        }

        [HttpPost]
        public dynamic Create(UserModel model)
        {
            if (model.PictureFile.IsNotNull())
            {
                var picturelBlob = new Blob();
                using (var memoryStream = new MemoryStream())
                {
                    model.PictureFile.CopyTo(memoryStream);
                    picturelBlob.Content = memoryStream.ToArray();
                    picturelBlob.Name = model.PictureFile.FileName;
                }
                userBusiness.Create(picturelBlob, model.FullName, model.Username, model.Password, model.MobileNumber, model.Email);
            }
            else
            {
                userBusiness.Update(model.Id, null, model.FullName, model.Username, model.Password, model.MobileNumber, model.Email);
            }

            return Ok("created");
        }

        [HttpPost]
        public dynamic Update(UserModel model)
        {
            if (model.PictureFile.IsNotNull())
            {
                var picturelBlob = new Blob();
                using (var memoryStream = new MemoryStream())
                {
                    model.PictureFile.CopyTo(memoryStream);
                    picturelBlob.Content = memoryStream.ToArray();
                    picturelBlob.Name = model.PictureFile.FileName;
                }
                userBusiness.Update(model.Id, picturelBlob, model.FullName, model.Username, model.Password, model.MobileNumber, model.Email);
            }
            else
            {
                userBusiness.Update(model.Id, null, model.FullName, model.Username, model.Password, model.MobileNumber, model.Email);
            }

            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var user = userBusiness.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] long id)
        {
            userBusiness.Delete(id);
            return Ok("Deleted");
        }

    }
}
