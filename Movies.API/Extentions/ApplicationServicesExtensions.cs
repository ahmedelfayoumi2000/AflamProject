using Movies.API.Helpers;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.BLL.Services;

namespace Movies.API.Extentions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<IMoviesService, MoviesService>(); 
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
