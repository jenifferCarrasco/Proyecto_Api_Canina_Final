using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLICATION.DTOs.User
{
	public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string PropietarioId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles{ get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public bool IsVerified { get; set; }
		[JsonIgnore]
		public string RefreshToken { get; set; }
    }
}
