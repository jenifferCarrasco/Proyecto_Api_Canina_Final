using Application.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
	public static class ServiceExtensions
    {
        public static void AddSharedInfraestructure(this IServiceCollection service, IConfiguration configuration) {

            service.AddTransient<IDateTimeService, DateTimeServices>();
        }
    }
}
