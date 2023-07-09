using APLICATION.Feauters.Canino.Commands.CreateCommand;
using APLICATION.Feauters.Canino.Queries.GetAllCanino;
using APLICATION.Feauters.Caninos.Commands.UpdateCommand;
using APLICATION.Feauters.Caninos.Queries.GetAllCanino;
using APLICATION.Feauters.Caninos.Queries.GetAllVacunacionesByCaninoId;
using APLICATION.Feauters.Caninos.Queries.GetCaninoById;
using APLICATION.Feauters.Clientes.Commands.DeleteClientCommand;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
	public class CaninosController : BaseApiController
	{
		[HttpGet]
		[SwaggerOperation(Summary = "Only Administrador: Obtener Caninos")]
		//[Authorize(Roles = "Admin")]
		//[Authorize(Roles = "Moderador")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllCaninoParameter filter)
		{
			return Ok(await Mediator.Send(new GetAllCaninoQuery
			{
				PageNumber = filter.PageNumber,
				PageSize = filter.PageSize,
				Nombre = filter.Nombre,
				Raza = filter.Raza
			}));
		}

		[HttpGet("{caninoId}/Vacunaciones")]
		[SwaggerOperation(Summary = "Only Propietario: Obtener vacunaciones de un canino")]

		public async Task<IActionResult> GetAllVacunaciones([FromRoute] Guid caninoId)
		{
			return Ok(await Mediator.Send(new GetAllVacunacionesByCaninoIdQuery
			{
				CaninoId = caninoId
			}));
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "All Users: Obtener un canino por su id")]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new GetCaninoByIdQuery { Id = id }));
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Only Propietario: Crear Canino")]
		public async Task<IActionResult> Post(CreateCaninoCommand createClientCommand)
		{
			return Ok(await Mediator.Send(createClientCommand));
		}

		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Only Propietario: Actualizar canino")]

		public async Task<IActionResult> Put(Guid id, UpdateCaninoCommand updateClientCommand)
		{
			if (id != updateClientCommand.Id)
				return BadRequest();
			return Ok(await Mediator.Send(updateClientCommand));
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Only Propietario: Eliminar Canino")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await Mediator.Send(new DeleteCaninoCommand { Id = id }));
		}
	}
}
