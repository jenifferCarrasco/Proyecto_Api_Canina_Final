using APLICATION.Parameters;

namespace APLICATION.Feauters.Admin.Queries.GetAllAdmin
{
	public class GetAllAdminParameter : RequestParameter
	{
		public string Email { get; set; }
		public string Username { get; set; }
	}
}
