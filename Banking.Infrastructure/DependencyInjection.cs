using Banking.Application;
using Banking.Infrastructure.Behaviors;
using Banking.Infrastructure.Data;
using Banking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Banking.Domain.Accounts;

namespace Banking.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventBehavior<,>));

        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
