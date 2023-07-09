using APLICATION.Feauters.Vacunadores.Commands.CreateCommand;
using APLICATION.Feauters.Vacunadores.Commands.DeleteCommand;
using APLICATION.Feauters.Vacunadores.Commands.UpdateCommand;
using APLICATION.Feauters.Vacunadores.Queries.GetAllVacunadores;
using APLICATION.Feauters.Vacunadores.Queries.GetVacunadoresById;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class VacunadorController : BaseApiController
    {
        [HttpGet]
		[SwaggerOperation(Summary = "All Users: Obtener Vacunadores")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllVacunadoresParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllVacunadoresQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre = filter.Nombre,
                Cedula = filter.Cedula
            }));
        }

        //Get api/<controller>/5
        [HttpGet("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Obtener vacunador")]
		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetVacunadoresByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
		[SwaggerOperation(Summary = "Only Administrador: Crear vacunador")]
		public async Task<IActionResult> Post(CreateVacunadoresCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Actualizar vacunador")]
		public async Task<IActionResult> Put(Guid id, UpdateVacunadoresCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Only Administrador: Eliminar vacunador")]
		public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await Mediator.Send(new DeleteVacunadoresCommand { Id = id }));
        }
    }
}
