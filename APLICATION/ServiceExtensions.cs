using APLICATION.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
	public static class ServiceExtensions
    {
        public static void AddAplicationLayer(this IServiceCollection service) {

            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavieour<,>));

        }
    }
}
