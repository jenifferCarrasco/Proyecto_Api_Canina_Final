using APLICATION.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Citas.Queries.GetAllCitas
{
    public class GetAllCitasParameter : RequestParameter
    {
        public DateTime FechaCita { get; set; }
        public Guid CaninoId { get; set; }
    }
}
