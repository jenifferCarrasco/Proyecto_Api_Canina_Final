using APLICATION.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Caninos.Queries.GetAllCanino
{
    public class GetAllCaninoParameter: RequestParameter
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
    }
}
