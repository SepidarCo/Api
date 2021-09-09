using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepidar.Api.DataAccess.DbContexts;
using Sepidar.Business.Interfaces;

namespace Sepidar.Business
{

    public class CalculationService
    {
        private DbContext context;

        public CalculationService(DbContext context)
        {
            this.context = context;
        }
    }
}

    //public class ApiDbContext : IApiDbContext
    //{
    //    private AppDbContext context;
    //    private IConfiguration Configuration;

    //    public ApiDbContext()
    //    {

    //    }

    //    public ApiDbContext(AppDbContext context, IConfiguration configuration, IServiceCollection services)
    //    {
    //        this.context = context;
    //        this.Configuration = configuration;

    //        services.AddDbContextPool<AppDbContext>(options =>
    //        {
    //            var connectionString = Configuration.GetConnectionString("Api");
    //            options.UseSqlServer(connectionString);
    //        });
    //    }
 