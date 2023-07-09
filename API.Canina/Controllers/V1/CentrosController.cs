using APLICATION.Feauters.Centros.Commands.CreateCentroCommand;
using APLICATION.Feauters.Centros.Commands.DeleteCentroCommand;
using APLICATION.Feauters.Centros.Commands.UpdateCentroCommand;
using APLICATION.Feauters.Centros.Queries.GetAllCentro;
using APLICATION.Feauters.Centros.Queries.GetCentroById;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]
    public class CentrosController : BaseApiController
    {
        [HttpGet]
		[SwaggerOperation(Summary = "All Users: Obtener Centros")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllCentroQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
		[SwaggerOperation(Summary = "All Users: Obtener Centro")]
		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetCentroByIdQuery { Id = id }));
        }

        [HttpPost]
		[SwaggerOperation(Summary = "Only Administrador: Crear Centro")]
		public async Task<IActionResult> Post(CreateCentroCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }

        [HttpPut("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Actualizar Centro")]
		public async Task<IActionResult> Put(Guid id, UpdateCentroCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }

        [HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Actualizar Centro")]
		public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCentroCommand { Id = id }));
        }
    }
}
