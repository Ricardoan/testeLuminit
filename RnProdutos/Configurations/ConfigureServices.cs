
using MsComercio.Domain.Entities.PreRota;
using MsComercio.Repository.PreApproval;
using MsComercio.Domain.Applications;
using MsComercio.Application;
using MsComercio.Domain.Business;
using MsComercio.Repository.RotaViagem;
using MsComercio.Business.PreBusiness;


namespace Api.Configurations
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAplications(this IServiceCollection services)
        {
            services.AddScoped<IPreApplication, PreApplication>();
            return services;
        }
        // OBS: Caso nescessário pode-se passar como parametro, IConfiguration configuration:
        public static IServiceCollection AddAdapters(this IServiceCollection services) 
        {
            services.AddTransient<IPreRepository, PreRepository>();
            //services.AddTransient<IInsuredAdapter, InsuredRepository>();
            return services;

        }
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IPreBusiness,PreBusiness>();
            return services;

        }
               


    }
}
