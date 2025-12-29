global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using System.Globalization;
global using System.Reflection;
using NewYou.Application.Behaviors;
namespace NewYou.Application;
public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("az");


        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

    }
}