using Sepidar.Api.DataAccess.Models;
using Sepidar.Framework;
using Sepidar.BlobManagement;
using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;
using Sepidar.Api.DataAccess.Models.Module;
using Sepidar.Business.Interfaces;

namespace Sepidar.Business
{
    public class SliderBusiness : ISliderBusiness
    {
        private IMediaBusiness mediaBusiness;
        private Repository<Slider> sliderRepository;

        public SliderBusiness(Repository<Slider> sliderRepository, IMediaBusiness mediaBusiness)
        {
            this.sliderRepository = sliderRepository;
            this.mediaBusiness = mediaBusiness;
        }

        public void Create(Blob picturelBlob, long languageId, string title, bool isActive, long orderNumber, string description)
        {
            var token = mediaBusiness.Add(picturelBlob);

            var slider = new Slider
            {
                LanguageId = languageId,
                PictureToken = token,
                Title = title,
                Description = description,
                IsActive = isActive,
                OrderNumber = orderNumber
            };

            sliderRepository.Create(slider);
        }

        public void Delete(long id)
        {
            sliderRepository.Delete(id);
        }

        public dynamic Get(long id)
        {
            var model = sliderRepository.Get(id);

            if (model.IsNotNull())
            {
                dynamic picture = new
                {
                    Id = model.Id,
                    LanguageId = model.LanguageId,
                    Title = model.Title,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    OrderNumber = model.OrderNumber,
                    Token = model.PictureToken,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(model.PictureToken, (long?)MediaType.Image).ToString()
                };
                return picture;
            }
            else
            {
                throw new FrameworkException("Slider Picture does not exists");
            }
        }

        public dynamic List(long languageId, ListOptions listOptions)
        {
            var sliders = sliderRepository.All
              .Where(i => i.LanguageId == languageId)
              .OrderByDescending(i => i.OrderNumber > 0)
              .ThenBy(i => i.OrderNumber).ApplyListOptionsAndGetTotalCount(listOptions);

            var result = new ListResult<dynamic>();
            sliders.CopyMetaDataFrom(sliders, result);

            foreach (var slider in sliders.Data)
            {
                var model = new
                {
                    Id = slider.Id,
                    LanguageId = slider.LanguageId,
                    Title = slider.Title,
                    Description = slider.Description,
                    IsActive = slider.IsActive,
                    OrderNumber = slider.OrderNumber,
                    PictureToken = slider.PictureToken,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(slider.PictureToken, (long?)MediaType.Image).ToString()
                };
                result.Data.Add(model);
            }

            return result;
        }

        public dynamic SliderList(long languageId)
        {
            var sliders = sliderRepository.All
                .Where(i => i.LanguageId == languageId && i.IsActive == true)
                .OrderByDescending(i => i.OrderNumber > 0).ThenBy(i => i.OrderNumber).ToList();

            var result = new List<dynamic>();

            foreach (var slider in sliders)
            {
                var model = new
                {
                    Id = slider.Id,
                    Title = slider.Title,
                    IsActive = slider.IsActive,
                    OrderNumber = slider.OrderNumber,
                    Description = slider.Description,
                    LanguageId = slider.LanguageId,
                    PhotoUrl = mediaBusiness.MakeUrlFromFileToken(slider.PictureToken, (long?)MediaType.Image).ToString()
                };
                result.Add(model);
            }

            return result;
        }

        public void Update(long id, Blob? picturelBlob, long languageId, string title, bool isActive, long orderNumber, string description)
        {
            var model = sliderRepository.Get(id);

            if (picturelBlob.IsNotNull())
            {
                var token = mediaBusiness.Add(picturelBlob);
                model.PictureToken = token;
            }

            model.LanguageId = languageId;
            model.Title = title;
            model.Description = description;
            model.IsActive = isActive;
            model.OrderNumber = orderNumber;

            sliderRepository.Update(model);
        }
    }
}
