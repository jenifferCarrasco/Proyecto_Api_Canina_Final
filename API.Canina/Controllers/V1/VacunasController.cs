using APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.DeleteVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.UpdateInventarioCommand;
using APLICATION.Feauters.Vacunas.Commands.UpdateVacunaCommand;
using APLICATION.Feauters.Vacunas.Queries.GetAllVacuna;
using APLICATION.Feauters.Vacunas.Queries.GetVacunaById;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class VacunasController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet]
		[SwaggerOperation(Summary = "All Users: Obtener Vacunas")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllVacunaQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Obtener vacuna")]

		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetVacunaByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
		[SwaggerOperation(Summary = "Only Administrador: Crear Vacuna")]
		public async Task<IActionResult> Post(CreateVacunaCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }

		[HttpPut("Inventario/{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Actualizar inventario vacuna")]
		public async Task<IActionResult> Put(Guid id, UpdateInventarioCommand command)
		{
			if (id != command.Id)
				return BadRequest();
			return Ok(await Mediator.Send(command));
		}

		//PUT api/<controller>/5
		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Actualizar vacunas")]

		public async Task<IActionResult> Put(Guid id, UpdateVacunaCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Eliminar vacuna")]

		public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await Mediator.Send(new DeleteVacunaCommand { Id = id }));
        }
    }
}
