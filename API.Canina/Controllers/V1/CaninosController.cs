using APLICATION.Feauters.Canino.Commands.CreateCommand;
using APLICATION.Feauters.Canino.Queries.GetAllCanino;
using APLICATION.Feauters.Caninos.Commands.UpdateCommand;
using APLICATION.Feauters.Caninos.Queries.GetAllCanino;
using APLICATION.Feauters.Caninos.Queries.GetCaninoById;
using APLICATION.Feauters.Clientes.Commands.DeleteClientCommand;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
	public class CaninosController : BaseApiController
	{
		//Get api/<controller>
		[HttpGet]
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

		//Get api/<controller>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new GetCaninoByIdQuery { Id = id }));
		}

		//POST api/<controller>
		[HttpPost]
		public async Task<IActionResult> Post(CreateCaninoCommand createClientCommand)
		{
			return Ok(await Mediator.Send(createClientCommand));
		}
		//PUT api/<controller>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, UpdateCaninoCommand updateClientCommand)
		{
			if (id != updateClientCommand.Id)
				return BadRequest();
			return Ok(await Mediator.Send(updateClientCommand));
		}
		//DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok(await Mediator.Send(new DeleteCaninoCommand { Id = id }));
		}
	}
}
