using Books.Application.Contracts.Persistence;
using Books.Application.Contracts.Services;
using Books.Domain.UserAggregate;
using Books.Infrastructure.Authentication;
using Books.Infrastructure.Persistence;
using Books.Infrastructure.Persistence.Interceptors;
using Books.Infrastructure.Persistence.Repositories;
using Books.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Books.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<ICsvExporter, CsvExporter>();

        services.AddPersistence(configuration)
            .AddAuth(configuration);

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };

                options.Events = new JwtBearerEvents()
                {
                    //OnAuthenticationFailed = context =>
                    //{
                    //    context.NoResult();
                    //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    //    context.Response.ContentType = "application/json";

                    //    var internalServerProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "InternalServerError", Detail = "Authentication could not be completed" };
                    //    return context.Response.WriteAsJsonAsync(internalServerProblem);
                    //},
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var unauthorizedProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "Unauthorized" };

                        return context.Response.WriteAsJsonAsync(unauthorizedProblem);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var forbiddenProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "Forbidden" };
                        return context.Response.WriteAsJsonAsync(forbiddenProblem);
                    }
                };
            });

        services.AddAuthorization();
        return services;

    }
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<BooksDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnectionString");
            options.UseSqlite(connectionString);
        });

        services.AddScoped<PublishDomainEventInterceptor>();
        services.AddScoped<AuditableInterceptor>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IUserRespository, UserRepository>();
        return services;
    }

}
