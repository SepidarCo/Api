using Sepidar.BlobManagement;
using Sepidar.Framework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sepidar.Business.Interfaces;
using Sepidar.Service.Models;
using System.IO;
using Sepidar.Framework;

namespace Sepidar.Service.Controllers.Admin
{
    public class SliderController : AdminController
    {
        private ISliderBusiness sliderBusiness;

        public SliderController(ISliderBusiness sliderBusiness)
        {
            this.sliderBusiness = sliderBusiness;
        }

        [HttpGet]
        public dynamic List(long languageId, [ModelBinder] ListOptions listOptions)
        {
            var list = sliderBusiness.List(languageId, listOptions);
            return list;
        }

        [HttpPost]
        public dynamic Create(SliderModel model)
        {
            var picturelBlob = new Blob();
            using (var memoryStream = new MemoryStream())
            {
                model.PictureFile.CopyTo(memoryStream);
                picturelBlob.Content = memoryStream.ToArray();
                picturelBlob.Name = model.PictureFile.FileName;
            }

            sliderBusiness.Create(picturelBlob, model.LanguageId, model.Title, model.IsActive, model.OrderNumber, model.Description);

            return Ok("created");
        }

        [HttpPost]
        public dynamic Update(SliderModel model)
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
                sliderBusiness.Update(model.Id, picturelBlob, model.LanguageId, model.Title, model.IsActive, model.OrderNumber, model.Description);
            }
            else
            {
                sliderBusiness.Update(model.Id, null, model.LanguageId, model.Title, model.IsActive, model.OrderNumber, model.Description);
            }

            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var user = sliderBusiness.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] long id)
        {
            sliderBusiness.Delete(id);
            return Ok("Deleted");
        }
    }
}
