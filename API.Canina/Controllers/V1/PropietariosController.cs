using APLICATION.Feauters.Propietarios.Queries.GetAllPropietario;
using APLICATION.Feauters.Propietarios.Queries.GetCaninosByPropietarioId;
using APLICATION.Feauters.Propietarios.Queries.GetPropietarioById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class PropietariosController : BaseApiController
    {
        //Get api/<controller>
        [HttpGet]
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

		[HttpGet("{propietarioId}/Caninos")]
		public async Task<IActionResult> GetCaninosByPropietarioId(Guid propietarioId)
		{
			return Ok(await Mediator.Send(new GetCaninosByPropietarioIdQuery
			{
				PropietarioId = propietarioId
			}));
		}
    }
}
