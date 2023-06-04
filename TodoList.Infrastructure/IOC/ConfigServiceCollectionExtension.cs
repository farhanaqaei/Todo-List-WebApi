using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoList.Application.TodoServices.Implementations;
using TodoList.Application.TodoServices.Interfaces;
using TodoList.Application.UserServices.Implementations;
using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.UserAggregate.Entities;
using TodoList.Infrastructure.Context;

namespace TodoList.Infrastructure.IOC;

public static class ConfigServiceCollectionExtension
{
	public static void AddServices(this IServiceCollection services, IConfiguration config)
	{
		AddDbContext(services, config);
		AddJWtAuthentication(services, config);
		AddIdentityCore(services);


		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ITodoService, TodoService>();
		services.AddScoped<ITokenService, TokenService>();
	}

	public static void AddDbContext(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(config.GetConnectionString("TodoListDB"));
		});
	}

	public static void AddJWtAuthentication(this IServiceCollection services, IConfiguration config)
	{
		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidateIssuerSigningKey = true,
					ValidIssuer = config.GetSection("ValidIssuer").Value,
					ValidAudience = config.GetSection("ValidAudience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("SecretKey").Value))
				};
			});
	}

	public static void AddIdentityCore(this IServiceCollection services)
	{
		services.AddIdentityCore<User>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>();
	}
}
