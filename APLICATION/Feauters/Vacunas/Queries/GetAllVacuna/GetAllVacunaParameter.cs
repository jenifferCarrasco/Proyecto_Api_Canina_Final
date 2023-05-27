using APLICATION.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Vacunas.Queries.GetAllVacuna
{
    public class GetAllVacunaParameter: RequestParameter
    {
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
    }
}
