using Sepidar.Business.Interfaces;
using Sepidar.Framework;
using Sepidar.BlobManagement;
using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;
using Sepidar.Api.DataAccess.Models.Module;

namespace Sepidar.Business
{
    public class GalleryBusiness : IGalleryBusiness
    {
        private IMediaBusiness mediaBusiness;
        private Repository<Gallery> galleryRepository;

        public GalleryBusiness(Repository<Gallery> galleryRepository, IMediaBusiness mediaBusiness)
        {
            this.galleryRepository = galleryRepository;
            this.mediaBusiness = mediaBusiness;
        }

        public dynamic List(long languageId, ListOptions listOptions)
        {
            var pictures = galleryRepository.All
                .Where(i => i.LanguageId == languageId)
                .OrderByDescending(i => i.OrderNumber > 0)
                .ThenBy(i => i.OrderNumber).ApplyListOptionsAndGetTotalCount(listOptions);

            var result = new ListResult<dynamic>();
            pictures.CopyMetaDataFrom(pictures, result);

            foreach (var picture in pictures.Data)
            {
                var model = new
                {
                    Id = picture.Id,
                    PictureToken = picture.PictureToken,
                    LanguageId = picture.LanguageId,
                    Title = picture.Title,
                    Alt = picture.Alt,
                    IsActive = picture.IsActive,
                    OrderNumber = picture.OrderNumber,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(picture.PictureToken, (long?)MediaType.Image).ToString()
                };

                result.Data.Add(model);
            }

            return result;
        }

        public void Create(Blob picturelBlob, long languageId, string alt, string title, bool isActive, long orderNumber)
        {
            var token = mediaBusiness.Add(picturelBlob);

            var gallery = new Gallery
            {
                PictureToken = token,
                LanguageId = languageId,
                Alt = alt,
                Title = title,
                IsActive = isActive,
                OrderNumber = orderNumber
            };

            galleryRepository.Create(gallery);
        }

        public void Delete(long id)
        {
            galleryRepository.Delete(id);
        }

        public dynamic Get(long id)
        {
            var model = galleryRepository.Get(id);

            if (model.IsNotNull())
            {
                dynamic picture = new
                {
                    Id = model.Id,
                    Token = model.PictureToken,
                    Title = model.Title,
                    LanguageId = model.LanguageId,
                    Alt = model.Alt,
                    IsActive = model.IsActive,
                    orderNumber = model.OrderNumber,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(model.PictureToken, (long?)MediaType.Image).ToString()
                };
                return picture;
            }
            else
            {
                throw new FrameworkException("Picture does not exists");
            }
        }

        public void Update(long id, Blob picturelBlob, long languageId, string alt, string title, bool isActive, long orderNumber)
        {
            var model = galleryRepository.Get(id);

            model.Alt = alt;
            model.Title = title;
            model.LanguageId = languageId;
            model.IsActive = isActive;
            model.OrderNumber = orderNumber;

            if (picturelBlob.IsNotNull())
            {
                var token = mediaBusiness.Add(picturelBlob);
                model.PictureToken = token;
            }

            galleryRepository.Update(model);
        }

        public dynamic GalleryList()
        {
            var pictures = galleryRepository.All.Where(i => i.IsActive != false).OrderByDescending(i => i.OrderNumber > 0).ThenBy(i => i.OrderNumber).ToList();
            var result = new List<dynamic>();

            foreach (var picture in pictures)
            {
                var model = new
                {
                    Id = picture.Id,
                    PictureToken = picture.PictureToken,
                    Title = picture.Title,
                    Alt = picture.Alt,
                    IsActive = picture.IsActive,
                    orderNumber = picture.OrderNumber,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(picture.PictureToken, (long?)MediaType.Image).ToString()
                };
                result.Add(model);
            }

            return result;
        }
    }
}
