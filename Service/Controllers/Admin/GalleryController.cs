using Microsoft.AspNetCore.Mvc;
using Sepidar.BlobManagement;
using Sepidar.Business.Interfaces;
using Sepidar.Framework;
using Sepidar.Framework.Extensions;
using Sepidar.Service.Models;
using System.IO;

namespace Sepidar.Service.Controllers.Admin
{
    public class GalleryController : AdminController
    {
        private IGalleryBusiness galleryBusiness;

        public GalleryController(IGalleryBusiness galleryBusiness)
        {
            this.galleryBusiness = galleryBusiness;
        }

        [HttpGet]
        public dynamic List(long languageId, [ModelBinder] ListOptions listOptions)
        {
            var list = galleryBusiness.List(languageId, listOptions);
            return list;
        }

        [HttpPost]
        public dynamic Create(GalleryModel model)
        {
            var picturelBlob = new Blob();
            using (var memoryStream = new MemoryStream())
            {
                model.PictureFile.CopyTo(memoryStream);
                picturelBlob.Content = memoryStream.ToArray();
                picturelBlob.Name = model.PictureFile.FileName;
            }

            galleryBusiness.Create(picturelBlob, model.LanguageId, model.Alt, model.Title, model.IsActive, model.OrderNumber);

            return Ok("created");
        }

        [HttpPost]
        public dynamic Update(GalleryModel model)
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
                galleryBusiness.Update(model.Id, picturelBlob, model.LanguageId, model.Alt, model.Title, model.IsActive, model.OrderNumber);
            }
            else
            {
                galleryBusiness.Update(model.Id, null, model.LanguageId, model.Alt, model.Title, model.IsActive, model.OrderNumber);
            }

            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var user = galleryBusiness.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Delete([FromQuery] long id)
        {
            galleryBusiness.Delete(id);
            return Ok("Deleted");
        }
    }
}
