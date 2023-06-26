using APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.DeleteVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.UpdateInventarioCommand;
using APLICATION.Feauters.Vacunas.Commands.UpdateVacunaCommand;
using APLICATION.Feauters.Vacunas.Queries.GetAllVacuna;
using APLICATION.Feauters.Vacunas.Queries.GetVacunaById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class VacunasController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetAllVacunaParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllVacunaQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre = filter.Nombre,
            }));
        }

        //Get api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetVacunaByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post(CreateVacunaCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }

		[HttpPut("Inventario/{id}")]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Put(Guid id, UpdateInventarioCommand command)
		{
			if (id != command.Id)
				return BadRequest();
			return Ok(await Mediator.Send(command));
		}

		//PUT api/<controller>/5
		[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, UpdateVacunaCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await Mediator.Send(new DeleteVacunaCommand { Id = id }));
        }
    }
}
