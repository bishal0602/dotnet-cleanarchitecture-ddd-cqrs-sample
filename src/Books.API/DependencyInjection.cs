using Books.API.Mappings;
using Books.API.Services;
using Books.Application.Contracts.Services;

namespace Books.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            services.AddMappings();
            return services;
        }
    }
}
