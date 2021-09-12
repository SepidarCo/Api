using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepidar.Business;
using Sepidar.Business.Interfaces;
using Sepidar.Api.DataAccess.DbContexts;
using Sepidar.BaseApi;
using Sepidar.EntityFramework;

namespace Sepidar.Service
{
    public class BaseStartup : GeneralStartup
    {
        public BaseStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void RegisterDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("Api");
                options.UseSqlServer(connectionString);
            });
        }
        
        protected override void RegisterRepositories(IServiceCollection services)
        {
            
            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Security.User>, Sepidar.Api.DataAccess.Repositories.Security.UserRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Module.Calendar>, Sepidar.Api.DataAccess.Repositories.Module.CalendarRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Module.Content>, Sepidar.Api.DataAccess.Repositories.Module.ContentRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Module.Gallery>, Sepidar.Api.DataAccess.Repositories.Module.GalleryRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Module.Slider>, Sepidar.Api.DataAccess.Repositories.Module.SliderRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Security.Admin>, Sepidar.Api.DataAccess.Repositories.Security.AdminRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Security.EndUser>, Sepidar.Api.DataAccess.Repositories.Security.EndUserRepository>();

            services.AddScoped<Repository<Sepidar.Api.DataAccess.Models.Module.Setting>, Sepidar.Api.DataAccess.Repositories.Module.SettingRepository>();

        }

        protected override void RegisterBusinesses(IServiceCollection services)
        {
            
            services.AddScoped<IAdminBusiness, AdminBusiness>();

            services.AddScoped<ICalendarBusiness, CalendarBusiness>();

            services.AddScoped<IContentBusiness, ContentBusiness>();

            services.AddScoped<IEndUserBusiness, EndUserBusiness>();

            services.AddScoped<IGalleryBusiness, GalleryBusiness>();

            services.AddScoped<ISettingBusiness, SettingBusiness>();

            services.AddScoped<ISliderBusiness, SliderBusiness>();

            services.AddScoped<IUserBusiness, UserBusiness>();

        }
    }
}
