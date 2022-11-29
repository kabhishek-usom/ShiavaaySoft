using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShivaaySoft.Repositories;
using ShivaaySoft.Repositories.Implementations;
using ShivaaySoft.Repositories.Interfaces;

namespace ShivaaySoft.Web.Configuration
{
    public class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                        Configuration.GetConnectionString("ShivaaySoftDb")));
            services.AddScoped<IEnquiryTypeRepository, EnquiryTypeRepository>();
            services.AddScoped<IEnquiryRepository, EnquiryRepository>();
        }
    }
}
