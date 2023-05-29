using APLICATION.Parameters;
using System;

namespace APLICATION.Feauters.Vacunaciones.Queries.GetAllVacunacion
{
	public class GetAllVacunacionParameter: RequestParameter
    {
        public Guid CaninoId { get; set; }
        public Guid VacunadorId { get; set; }
    }
}
