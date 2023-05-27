using APLICATION.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Vacunaciones.Queries.GetAllVacunacion
{
    public class GetAllVacunacionParameter: RequestParameter
    {
        public Guid CaninoId { get; set; }
        public Guid VacunadorId { get; set; }
    }
}
