using Sepidar.Api.DataAccess.Models.Module;
using Sepidar.BlobManagement;
using Sepidar.Business.Interfaces;
using Sepidar.EntityFramework;
using Sepidar.Framework;
using Sepidar.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Sepidar.Business
{
    public class ContentBusiness : IContentBusiness
    {
        private IMediaBusiness mediaBusiness;
        private Repository<Content> contentRepository;

        public ContentBusiness(Repository<Content> contentRepository, IMediaBusiness mediaBusiness)
        {
            this.contentRepository = contentRepository;
            this.mediaBusiness = mediaBusiness;
        }

        public dynamic Get(long id)
        {
            var content = contentRepository.Get(id);
            return content;
        }

        public dynamic List(long languageId)
        {
            var list = contentRepository.All.Where(i => i.LanguageId == languageId).OrderByDescending(i => i.OrderNumber > 0).ThenBy(i => i.OrderNumber).ToList();
            return list;
        }

        public void Update(long id , Blob picturelBlob, long languageId, string title, string description , bool isActive, long orderNumber)
        {
            var model = contentRepository.Get(id);

            if (picturelBlob.IsNotNull())
            {
                var token = mediaBusiness.Add(picturelBlob);
                model.PictureToken = token;
            }

            model.Title = title;
            model.Description = description;
            model.IsActive = isActive;
            model.OrderNumber = orderNumber;

            contentRepository.Update(model);
        }

        public dynamic ContentList(long languageId)
        {
            var list = contentRepository.All.Where(i => i.LanguageId == languageId && i.IsActive == true).OrderByDescending(i => i.OrderNumber > 0).ThenBy(i => i.OrderNumber).ToList();
           
            var result = new List<dynamic>();
          
            foreach (var item in list)
            {
                var model = new
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsActive = item.IsActive,
                    OrderNumber = item.OrderNumber,
                    Description = item.Description,
                    LanguageId = item.LanguageId,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(item.PictureToken, (long?)MediaType.Image).ToString()
                };
                result.Add(model);
            }

            return result;
        }
    }
}
