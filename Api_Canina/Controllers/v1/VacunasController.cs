using APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.DeleteVacunaCommand;
using APLICATION.Feauters.Vacunas.Commands.UpdateVacunaCommand;
using APLICATION.Feauters.Vacunas.Queries.GetAllVacuna;
using APLICATION.Feauters.Vacunas.Queries.GetVacunaById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Canina.Controllers.v1
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
                Laboratorio = filter.Laboratorio
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
