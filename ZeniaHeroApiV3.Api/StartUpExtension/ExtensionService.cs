using ZeniaHeroApiV3.Business.Abstract;
using ZeniaHeroApiV3.Business.Concrete;

namespace ZeniaHeroApiV3.Api.StartUpExtension
{
    public static class ExtensionService
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<ISalesReportService, SalesReportManager>();
        }
    }
}
