namespace Books.API.Mappings
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));
            return services;
        }
    }
}
