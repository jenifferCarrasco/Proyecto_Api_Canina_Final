using APLICATION.Feauters.Propietarios.Commands.CreateCommand;
using APLICATION.Feauters.Propietarios.Commands.DeleteCommand;
using APLICATION.Feauters.Propietarios.Commands.UpdateCommand;
using APLICATION.Feauters.Propietarios.Queries.GetAllPropietario;
using APLICATION.Feauters.Propietarios.Queries.GetPropietarioById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api_Canina.Controllers.v1
{

    [ApiVersion("1.0")]
    public class PropietarioController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPropietarioParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllPropietarioQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre = filter.Nombre,
                Cedula = filter.Cedula
            }));
        }

        //Get api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetPropietarioByIdQuery { Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Moderador")]
        public async Task<IActionResult> Post(CreatePropietarioCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderador")]
        public async Task<IActionResult> Put(Guid id, UpdatePropietarioCommand updateClientCommand)
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

            return Ok(await Mediator.Send(new DeletePropietarioCommand { Id = id }));
        }
    }
}
