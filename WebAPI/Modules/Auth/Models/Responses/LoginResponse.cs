namespace WebAPI.Modules.Auth.Models.Responses
{
    public record LoginResponse
    {
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public int IdUser { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiredTime { get; set; }

    }
}
