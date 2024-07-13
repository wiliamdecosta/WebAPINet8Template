using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modules.Auth.Models.Requests
{
	public class EndpointRoleRequest
	{
		[Required(ErrorMessage = "Role Id dibutuhkan")]
		public int RoleId { get; set; }

		[Required(ErrorMessage = "Endpoint Id dibutuhkan")]
		public int EndpointId { get; set; }
	}
}
