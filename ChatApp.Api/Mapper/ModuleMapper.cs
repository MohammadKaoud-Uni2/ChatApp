namespace ChatApp.Api.Mapper
{
    public  static class ModuleMapper
    {
        public static IServiceCollection MapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            return services;
        }
    }
}
