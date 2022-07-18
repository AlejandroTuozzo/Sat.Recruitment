using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Business.Interfaces;
using Sat.Recruitment.Api.DataAccess;

namespace Sat.Recruitment.Api.IoC
{
    public static class IoCExtension
    {
        public static IServiceCollection BindUserBusiness(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection BindUserRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection BindUserHelper(this IServiceCollection services)
        {
            services.AddSingleton<IUserHelper, UserHelper>();
            return services;
        }
    }
}
