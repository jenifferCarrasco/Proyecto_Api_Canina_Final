using APLICATION.Feauters.Admin.Commands.DeleteAdmin;
using APLICATION.Feauters.Admin.Queries.GetAdminById;
using APLICATION.Feauters.Admin.Queries.GetAllAdmin;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.V1
{
	[ApiVersion("1.0")]
	public class AdministradoresController : BaseApiController
	{
		[HttpGet]
		[SwaggerOperation(Summary = "Only Administrador: Obtener usuarios Administradores")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllAdminParameter filter)
		{
			return Ok(await Mediator.Send(new GetAllAdminQuery
			{
				PageNumber = filter.PageNumber,
				PageSize = filter.PageSize,
				Email = filter.Email,
				Username = filter.Username
			}));
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Obtener administrador")]
		public async Task<IActionResult> GetAdmin(Guid id)
		{
			return Ok(await Mediator.Send(new GetAdminByIdQuery
			{
				Id = id
			}));
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Eliminar Administrador")]
		public async Task<IActionResult> DeleteAdmin(Guid id)
		{
			return Ok(await Mediator.Send(new DeleteAdminCommand
			{
				Id= id
			}));
		}
	}
}
