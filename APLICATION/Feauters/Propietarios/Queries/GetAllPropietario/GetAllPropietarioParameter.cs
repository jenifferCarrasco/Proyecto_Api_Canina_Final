using APLICATION.Parameters;

namespace APLICATION.Feauters.Propietarios.Queries.GetAllPropietario
{
    public class GetAllPropietarioParameter : RequestParameter
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
    }

}
