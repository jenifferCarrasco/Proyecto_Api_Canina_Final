using APLICATION.Parameters;

namespace APLICATION.Feauters.Caninos.Queries.GetAllCanino
{
	public class GetAllCaninoParameter: RequestParameter
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
    }
}
