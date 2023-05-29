using DOMAIN.Canina.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var userManager = services.GetRequiredService<UserManager<Usuario>>();
					var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

					await DefaultRoles.SeedAsync(roleManager);
					await DefaultAdminUser.SeedAsync(userManager);
					await DefaultPacienteUser.SeedAsync(userManager);
				}
				catch (Exception)
				{

					throw;
				}
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
