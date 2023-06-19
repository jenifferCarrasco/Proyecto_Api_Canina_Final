using APLICATION.Feauters.Citas.Commands.CreateCitasCommand;
using APLICATION.Feauters.Citas.Commands.DeleteCitasCommand;
using APLICATION.Feauters.Citas.Commands.DesactivarCitaCommand;
using APLICATION.Feauters.Citas.Commands.UpdateCitasCommand;
using APLICATION.Feauters.Citas.Queries.GetAllCitas;
using APLICATION.Feauters.Citas.Queries.GetCitasById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
	public class CitasController : BaseApiController
	{
		//Get api/<controller>
		[HttpGet()]
		public async Task<IActionResult> GetAll([FromQuery] GetAllCitasParameter filter)
		{
			return Ok(await Mediator.Send(new GetAllCitasQuery
			{
				PageNumber = filter.PageNumber,
				PageSize = filter.PageSize,
				CaninoId = filter.CaninoId
			}));
		}

		//Get api/<controller>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new GetCitasByIdQuery { Id = id }));
		}

		//POST api/<controller>
		[HttpPost]
		//[Authorize(Roles = "Admin")]
		//[Authorize(Roles = "Moderador")]
		public async Task<IActionResult> Post(CreateCitaCommand createClientCommand)
		{
			return Ok(await Mediator.Send(createClientCommand));
		}
		//PUT api/<controller>/5
		[HttpPut("{id}")]
		//[Authorize(Roles = "Admin")]
		//[Authorize(Roles = "Moderador")]
		public async Task<IActionResult> Put(Guid id, UpdateCitasCommand updateClientCommand)
		{
			if (id != updateClientCommand.Id)
				return BadRequest();
			return Ok(await Mediator.Send(updateClientCommand));
		}

		[HttpPut("Desactivar/{id}")]
		public async Task<IActionResult> Desactivar(Guid id)
		{
			return Ok(await Mediator.Send(new DesactivarCitaCommand
			{
				Id = id
			}));
		}

		//DELETE api/<controller>/5
		[HttpDelete("{id}")]
		//[Authorize(Roles = "Admin")]
		//[Authorize(Roles = "Moderador")]
		public async Task<IActionResult> Delete(Guid id)
		{

			return Ok(await Mediator.Send(new DeleteCitasCommand { Id = id }));
		}
	}
}
