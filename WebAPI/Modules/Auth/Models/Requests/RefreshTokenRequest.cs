using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modules.Auth.Models.Requests
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Token dibutuhkan")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Refresh token dibutuhkan")]
        public string RefreshToken { get; set; }
    }
}
