using Microsoft.AspNetCore.Mvc;
using Sepidar.BaseApi;
using Sepidar.Business.Interfaces;
using Sepidar.Framework;

namespace Sepidar.Radio.Service.Controllers
{
    [IsPublic]
    public class ModuleController : GeneralController
    {
        private ISliderBusiness sliderBusiness;
        private ICalendarBusiness calendarBusiness;
        private IContentBusiness contentBusiness;
        private IGalleryBusiness galleryBusiness;
        private ISettingBusiness settingBusiness;

        public ModuleController(ISliderBusiness sliderBusiness, ICalendarBusiness calendarBusiness, IContentBusiness contentBusiness, IGalleryBusiness galleryBusiness, ISettingBusiness settingBusiness)
        {
            this.sliderBusiness = sliderBusiness;
            this.calendarBusiness = calendarBusiness;
            this.contentBusiness = contentBusiness;
            this.galleryBusiness = galleryBusiness;
            this.settingBusiness = settingBusiness;
        }

        [HttpGet]
        public dynamic CalendarList(long languageId)
        {
            var list = calendarBusiness.CalendarList(languageId);
            return list;
        }

        [HttpGet]
        public dynamic ContentList(long languageId)
        {
            var list = contentBusiness.ContentList(languageId);
            return list;
        }

        [HttpGet]
        public dynamic GetContent(long id)
        {
            var content = contentBusiness.Get(id);
            return content;
        }

        [HttpGet]
        public dynamic GalleryList()
        {
            var list = galleryBusiness.GalleryList();
            return list;
        }

        [HttpGet]
        public dynamic Settings()
        {
            long id = 1;
            var setting = settingBusiness.Get(id);
            return setting;
        }

        [HttpGet]
        public dynamic SliderList(long languageId)
        {
            var list = sliderBusiness.SliderList(languageId);
            return list;
        }
    }
}

