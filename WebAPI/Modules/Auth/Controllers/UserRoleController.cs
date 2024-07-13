using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Modules.Auth.Models.Requests;
using WebAPI.Modules.Auth.Services;

namespace WebAPI.Modules.Auth.Controllers
{
	[Route("api/v1/userrole")]
	public class UserRoleController : ControllerBase
	{
		private readonly UserRoleService _service;

		public UserRoleController(UserRoleService service)
		{
			_service = service;
		}

		[HttpGet("list", Name = "GetListUserRoles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<UserRole>>> ListUserRole()
		{
			List<UserRole> userRole = _service.FetchAll();

			var responseData = BaseResponse<List<UserRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_USER_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("{id:int}", Name = "GetUserRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<UserRole>> GetUserRole(int id)
		{
			var userRole = _service.FetchOne(id);
			var responseData = BaseResponse<UserRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ONE_USER_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("list-by-user-id/{id:int}", Name = "GetUserRoleByUserId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<UserRole>>> GetUserRoleByUserId(int id)
		{
			var userRole = _service.FetchAllByUserId(id);
			var responseData = BaseResponse<List<UserRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_USER_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("list-by-role-id/{id:int}", Name = "GetUserRoleByRoleId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<UserRole>>> GetUserRoleByRoleId(int id)
		{
			var userRole = _service.FetchAllByRoleId(id);
			var responseData = BaseResponse<List<UserRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_USER_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("create", Name = "CreateUserRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<UserRole>> CreateRole([FromBody] UserRoleRequest request)
		{
			UserRole userRole = _service.Create(request);

			var responseData = BaseResponse<UserRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("CREATE_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpPut("{id:int}", Name = "UpdateUserRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<UserRole>> UpdateRole(int id, [FromBody] UserRoleRequest request)
		{
			var userRole = _service.Update(id, request);
			var responseData = BaseResponse<UserRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("UPDATE_ROLE_SUCCESS")
				.Data(userRole)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("delete", Name = "DeleteUserRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<string>>> DeleteRole([FromBody] DeleteRequest ids)
		{
			var deletedIds = _service.Delete(ids);
			var responseData = BaseResponse<List<string>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("USER_ROLES_DELETED")
				.Data(deletedIds)
				.Build();

			return Ok(responseData);
		}

	}
}
