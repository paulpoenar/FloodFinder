using System;
using System.Reflection;
using FloodFinder.Application.Contracts;
using FloodFinder.Application.Shared.Behaviours;
using FloodFinder.Infrastructure.Events;
using FloodFinder.Infrastructure.Persistence.EntityFramework;
using FloodFinder.Infrastructure.Security;
using FloodFinder.Infrastructure.Security.Identity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FloodFinder.Infrastructure
{
  public static class IoC
  {
    public static IServiceCollection SetupInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
          configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
      services.AddScoped<IDomainEventsPublisher, DomainEventsPublisher>();
      services.AddScoped<ICurrentUserService, CurrentUserService>();

      services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ApplicationDbContext>();

      services.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

      services.AddAuthentication()
        .AddIdentityServerJwt();

      services.AddAuthorization();

      return services;
    }
  }
}
