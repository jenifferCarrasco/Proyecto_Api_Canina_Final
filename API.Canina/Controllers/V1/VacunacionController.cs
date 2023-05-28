using APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand;
using APLICATION.Feauters.Vacunaciones.Commands.DeleteVacunacionCommand;
using APLICATION.Feauters.Vacunaciones.Commands.UpdateVacunacionCommand;
using APLICATION.Feauters.Vacunaciones.Queries.GetAllVacunacion;
using APLICATION.Feauters.Vacunaciones.Queries.GetVacunacionById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class VacunacionController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetAllVacunacionParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllVacunacionQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                VacunadorId = filter.VacunadorId,
                CaninoId = filter.CaninoId
            }));
        }

        //Get api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetVacunacionByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderador")]
        public async Task<IActionResult> Post(CreateVacunacionCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderador")]
        public async Task<IActionResult> Put(Guid id, UpdateVacunacionCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderador")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await Mediator.Send(new DeleteVacunacionCommand { Id = id }));
        }
    }
}
