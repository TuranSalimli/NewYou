using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewYou.Application.Abstraction.Services;
using NewYou.Domain.Entities;
using NewYou.Persistance.Contexts;
using NewYou.Persistence.Concretes.Services;

namespace NewYou.Persistance;

public static class ServiceRegistiration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<NewYouDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
  
        services.AddScoped<IEmailService, EmailService>();
   
        services.AddScoped<ITokenService, TokenService>();


        services.AddIdentity<Account, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;
            opt.SignIn.RequireConfirmedEmail = false;

            opt.SignIn.RequireConfirmedEmail = true;
            opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
         .AddDefaultTokenProviders()
         .AddEntityFrameworkStores<NewYouDbContext>();

    }
}