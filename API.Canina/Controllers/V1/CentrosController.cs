using APLICATION.Feauters.Centros.Commands.CreateCentroCommand;
using APLICATION.Feauters.Centros.Commands.DeleteCentroCommand;
using APLICATION.Feauters.Centros.Commands.UpdateCentroCommand;
using APLICATION.Feauters.Centros.Queries.GetAllCentro;
using APLICATION.Feauters.Centros.Queries.GetCentroById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]

    public class CentrosController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCentroParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllCentroQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre = filter.Nombre
            }));
        }

        //Get api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetCentroByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCentroCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCentroCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
                return BadRequest();
            return Ok(await Mediator.Send(updateClientCommand));
        }
        //DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCentroCommand { Id = id }));
        }
    }
}
