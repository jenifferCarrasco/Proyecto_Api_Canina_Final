using APLICATION.Parameters;

namespace APLICATION.Feauters.Vacunadores.Queries.GetAllVacunadores
{
    public class GetAllVacunadoresParameter : RequestParameter
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
    }
}
