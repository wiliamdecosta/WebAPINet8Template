using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modules.Auth.Models.Requests
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "Nama dibutuhkan")]
        [MaxLength(100, ErrorMessage = "Panjang karakter maksimal 100")]
        public string Name { get; set; }
    }
}
