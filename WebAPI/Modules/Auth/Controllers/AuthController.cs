
using Entities.Auth;
using Infrastructure.Models.Responses;
using Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Modules.Auth.Models.Requests;
using WebAPI.Modules.Auth.Models.Responses;
using WebAPI.Modules.Auth.Services;

namespace WebAPI.Modules.Auth.Controllers
{
	[Route("api/v1/auth")]
	public class AuthController : ControllerBase
	{
		private readonly UserService _service;
		private readonly IConfiguration _configuration;

		public AuthController(UserService service, IConfiguration configuration)
		{
			_service = service;
			_configuration = configuration;
		}

		[HttpPost("login", Name = "Login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<BaseResponse<LoginResponse>> Login([FromBody] LoginRequest request)
		{
			var loginResponse = new LoginResponse()
			{
				AccessToken = "",
				Email = "",
				IdUser = 0,
				RefreshToken = "",
				RefreshTokenExpiredTime = "",
			};

			var responseData = BaseResponse<LoginResponse>.Builder()
						.Code(StatusCodes.Status200OK)
						.Message("Username atau password salah");

			User? user = _service.FetchByEmail(request.Email);

			if (user != null)
			{
				if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
				{
					return BadRequest(responseData.Data(loginResponse).Code(StatusCodes.Status400BadRequest).Build());
				}
				else
				{
					
					var token = JwtUtil.GenerateJwtToken(user, _configuration);
					var refreshToken = JwtUtil.GenerateRefreshToken();
					var refreshTokenExpiryTime = DateTime.Now.AddDays(7); //1 minggu

					user.RefreshToken = refreshToken;
					user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
					_service.UpdateRefreshToken(user);
					loginResponse.AccessToken = token;
					loginResponse.RefreshToken = refreshToken;
					loginResponse.RefreshTokenExpiredTime = refreshTokenExpiryTime.ToString("o");
					loginResponse.Email = user.Email;
					loginResponse.IdUser = user.Id;

					return Ok(responseData.Data(loginResponse).Message("Login berhasil").Build());
				}
			}
			else
			{
				return BadRequest(responseData);
			}

		}

		[HttpPost("refresh_token", Name = "RefreshToken")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<BaseResponse<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
		{

			RefreshTokenResponse? result = _service.GetNewToken(request, _configuration);
			var responseData = BaseResponse<RefreshTokenResponse>.Builder()
						.Code(StatusCodes.Status200OK);

			if (result == null)
			{
				return BadRequest(responseData.Data(result)
					.Code(StatusCodes.Status400BadRequest)
					.Message("Token Invalid or Expired Refresh Token").Build());
			}

			return Ok(responseData.Data(result).Message("Refresh token berhasil").Build());
		}
	}
}
