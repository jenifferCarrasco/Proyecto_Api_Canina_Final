using API.Canina.Extensions;
using Application;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PERSISTENCE.Canina;
using Shared;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API.Canina
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAplicationLayer();
			services.AddSharedInfraestructure(Configuration);
			services.AddPersistenceInfraestructure(Configuration);
			services.AddControllers().AddJsonOptions(json =>
			{
				//json.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				json.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			services.AddApiVersioningExtension();
			services.AddValidatorsFromAssemblyContaining<Startup>();
			services.AddControllers();
			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api_Canina", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api_Canina v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();
			app.UseErrorHandlingMiddleware();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
