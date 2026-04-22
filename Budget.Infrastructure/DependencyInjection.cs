using Budget.Application;
using Budget.Domain.Interfaces;
using Budget.Infrastructure.Behaviors;
using Budget.Infrastructure.Data;
using Budget.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Budget.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, AppDbContext>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventBehavior<,>));

        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
