using ChatApp.Api.Repos.Abstract;
using ChatApp.Api.Repos.Implementation;

namespace ChatApp.Api.Repos.ModuleRepo
{
    public static  class RepoService
    {
       public static IServiceCollection AddRepoService(this IServiceCollection services)
        {
            services.AddScoped<ITokenRepo, TokenRepo>();
           services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ILikedRepo, LikedRepo>();
            services.AddScoped<IMessageRepo,MessageRepo>();
            return services;
        }
    }
}
