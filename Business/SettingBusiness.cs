using Sepidar.EntityFramework;
using Sepidar.Api.DataAccess.Models.Module;
using System.Linq;
using Sepidar.Business.Interfaces;

namespace Sepidar.Business
{
    public class SettingBusiness : ISettingBusiness
    {
        private Repository<Setting> settingRepository;

        public SettingBusiness(Repository<Setting> settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        public void Create(Repository<Setting> settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        public void Create(Setting setting)
        {
            settingRepository.Create(setting);
        }

        public void Delete(long id)
        {
            settingRepository.Delete(id);
        }

        public dynamic Get(long id)
        {
            var setting = settingRepository.Get(id);
            return setting;
        }

        public dynamic List()
        {
            var list = settingRepository.All.ToList();
            return list;
        }

        public void Update(Setting model)
        {
            var setting = settingRepository.Get(model.Id);

            setting.SiteName = model.SiteName;
            setting.Keyword = model.Keyword;
            setting.Description = model.Description;
            setting.Copyright = model.Copyright;
            setting.CompanyName = model.CompanyName;
            setting.Telephone = model.Telephone;
            setting.Email = model.Email;
            setting.Fax = model.Fax;
            setting.Address = model.Address;
            setting.Map = model.Map;
            setting.Instagram = model.Instagram;
            setting.Facebook = model.Facebook;
            setting.LinkedIn = model.LinkedIn;
            setting.TikTok = model.TikTok;
            setting.Youtube = model.Youtube;
            setting.Whatsapp = model.Whatsapp;
            setting.Twitter = model.Twitter;
            setting.Pinterest = model.Pinterest;

            settingRepository.Update(setting);
        }
    }
}
