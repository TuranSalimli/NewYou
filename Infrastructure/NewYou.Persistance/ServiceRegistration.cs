using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewYou.Application.Abstraction.Repositories.Command;
using NewYou.Application.Abstraction.Repositories.Query;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Domain.Entities;
using NewYou.Persistance.Contexts;
using NewYou.Persistence.Concretes.Repositories.Command;
using NewYou.Persistence.Concretes.Repositories.Query;
using NewYou.Persistence.Concretes.Services;
using NewYou.Persistence.UnitOfWorks;

namespace NewYou.Persistance;

public static class ServiceRegistiration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<NewYouDbContext>(opt => 
            opt.UseSqlServer(connectionString, sqlOptions => 
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,          
                    maxRetryDelay: TimeSpan.FromSeconds(30),  
                    errorNumbersToAdd: null);
            }));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped<ITokenService, TokenService>();

        services.AddIdentity<Account, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireDigit = false;

            opt.SignIn.RequireConfirmedEmail = true;
            opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<NewYouDbContext>();
    }
}