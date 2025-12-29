using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewYou.Domain.Entities;
using NewYou.Persistance.Contexts;

namespace NewYou.Persistance;

public static class ServiceRegistiration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<NewYouDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
        /*services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();*/


        services.AddIdentity<Account, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;
            opt.SignIn.RequireConfirmedEmail = false;
        })
         .AddDefaultTokenProviders()
         .AddEntityFrameworkStores<NewYouDbContext>();

    }
}