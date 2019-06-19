using Microsoft.Extensions.DependencyInjection;

using FACP.PasswordStrength.Service.Interfaces;
using FACP.PasswordStrength.Service.Implementations;
using RestSharp;

namespace FACP.PasswordStrength.Service.DI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordStrengthService, PasswordStrengthService>();
            services.AddTransient<IRestExternalService, RestExternalService>();
            services.AddTransient<IPwnedPasswordService, PwnedPasswordService>();
            services.AddTransient<IRestClient, RestClient>();
            return services;
        }
    }
}
