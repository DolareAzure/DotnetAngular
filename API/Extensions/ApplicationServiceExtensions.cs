using API.Data;
using API.Helpers;
using API.Interface;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("Default"));
            });

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IuserRepository, UserRepository>();

            // Auto Mapper Configurations
            // var mappingConfig = new MapperConfiguration(mc =>
            // {
            //     mc.AddProfile(new AutoMapperProfiles());
            // });

            // IMapper mapper = mappingConfig.CreateMapper();
            // services.AddSingleton(mapper);

             services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}