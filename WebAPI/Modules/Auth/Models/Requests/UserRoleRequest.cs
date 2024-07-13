using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modules.Auth.Models.Requests
{
    public class UserRoleRequest
    {
        [Required(ErrorMessage ="User Id dibutuhkan")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role Id dibutuhkan")]
        public int RoleId { get; set; }
    }
}
