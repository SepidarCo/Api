using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepidar.BlobManagement;

namespace Sepidar.Service
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            // Add More Services Here if Needed
            services.AddScoped<BlobRepository<DiskBlobStorage>, DiskBlobRepository>();
            services.AddScoped<BlobRepository<DatabaseBlobStorage>, DatabaseBlobRepository>();
            services.AddScoped<IMediaBusiness, MediaBusiness>();
        }
    }
}
