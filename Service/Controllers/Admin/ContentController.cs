using Sepidar.BlobManagement;
using Sepidar.Framework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sepidar.Service.Models;
using System.IO;
using Sepidar.Business.Interfaces;

namespace Sepidar.Service.Controllers.Admin
{
    public class ContentController : AdminController
    {
        private IContentBusiness contentBusiness;

        public ContentController(IContentBusiness contentBusiness)
        {
            this.contentBusiness = contentBusiness;
        }

        [HttpGet]
        public dynamic List(long languageId)
        {
            var list = contentBusiness.List(languageId);
            return list;
        }

        [HttpPost]
        public dynamic Update(ContentModel model)
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
                contentBusiness.Update(model.Id, picturelBlob, model.LanguageId, model.Title, model.Description, model.IsActive, model.OrderNumber);
            }
            else
            {
                contentBusiness.Update(model.Id, null, model.LanguageId, model.Title, model.Description, model.IsActive, model.OrderNumber);
            }
            
            return Ok("Updated");
        }

        [HttpGet]
        public dynamic Get(long id)
        {
            var content = contentBusiness.Get(id);
            return content;
        }
    }
}
