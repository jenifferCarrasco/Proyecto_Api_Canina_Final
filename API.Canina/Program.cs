using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PERSISTENCE.Canina.Context;
using PERSISTENCE.Canina.Seeds;
using System;
using System.Threading.Tasks;

namespace API.Canina
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				var dbContext = services.GetRequiredService<ApplicationDbContext>();
				if (dbContext.Database.IsSqlServer())
				{
					dbContext.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

				logger.LogError(ex, "An error occurred while migrating or seeding the database.");

				throw;
			}

			try
			{
				var userManager = services.GetRequiredService<UserManager<Usuario>>();
				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

				await DefaultRoles.SeedAsync(roleManager);
				await DefaultAdminUser.SeedAsync(userManager);
				await DefaultPropietarioUser.SeedAsync(userManager);
			}
			catch (Exception)
			{

				throw;
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				}).UseDefaultServiceProvider(options =>
				options.ValidateScopes = false);
	}
}
