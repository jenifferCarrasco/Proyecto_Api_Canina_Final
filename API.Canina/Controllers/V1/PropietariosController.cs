using APLICATION.Feauters.Citas.Queries.GetCitasByPropietarioId;
using APLICATION.Feauters.Propietarios.Queries.GetAllPropietario;
using APLICATION.Feauters.Propietarios.Queries.GetCaninosByPropietarioId;
using APLICATION.Feauters.Propietarios.Queries.GetPropietarioById;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Canina.Controllers.v1
{

	[ApiVersion("1.0")]
    public class PropietariosController : BaseApiController
    {
        [HttpGet]
		[SwaggerOperation(Summary = "Only Administrador: Obtener Propietarios")]
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

        [HttpGet("{id}")]
		[SwaggerOperation(Summary = "Only Propietario: Obtener Perfil de propietario")]
		public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetPropietarioByIdQuery { Id = id }));
        }


		[HttpGet("{propietarioId}/Caninos")]
		[SwaggerOperation(Summary = "Only Propietario: Obtener caninos de propietario")]
		public async Task<IActionResult> GetCaninosByPropietarioId(Guid propietarioId)
		{
			return Ok(await Mediator.Send(new GetCaninosByPropietarioIdQuery
			{
				PropietarioId = propietarioId
			}));
		}

		[HttpGet("{propietarioId}/Citas")]
		[SwaggerOperation(Summary = "Only Propietario: Obtener citas de propietario")]

		public async Task<IActionResult> GetCitasByPropietarioId(Guid propietarioId)
		{
			return Ok(await Mediator.Send(new GetCitasByPropietarioIdQuery
			{
				PropietarioId = propietarioId
			}));
		}
	}
}
