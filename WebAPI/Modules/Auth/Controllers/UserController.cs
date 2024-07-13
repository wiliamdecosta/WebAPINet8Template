
using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Requests.Filters;
using Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Modules.Auth.Models.Requests;
using WebAPI.Modules.Auth.Services;

namespace WebAPI.Modules.Auth.Controllers
{
	[Route("api/v1/user")]
	public class UserController : ControllerBase
	{
		private readonly UserService _service;

		public UserController(UserService service)
		{
			_service = service;
		}

		[HttpPost("all", Name = "GetAllUsers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize]
		public ActionResult<BaseResponse<List<User>>> GetAllUsers([FromBody] SearchRequest request)
		{
			Paginated<User> paginatedItem = _service.FetchAll(request);

			var responseData = BaseResponse<List<User>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ALL_USER_LIST")
				.Data(paginatedItem.Data.ToList())
				.Page(new PageResponse()
				{
					Total = paginatedItem.TotalCount,
					Size = paginatedItem.PageSize,
					TotalPage = paginatedItem.TotalPages,
					Current = paginatedItem.PageNumber,
				})
				.Build();

			return Ok(responseData);
		}

		[HttpGet("list", Name = "GetListUsers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<User>>> ListUser()
		{
			List<User> user = _service.FetchAll();

			var responseData = BaseResponse<List<User>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("REGISTER_USER_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("{id:int}", Name = "GetUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<User>> FetchUser(int id)
		{
			var user = _service.FetchOne(id);
			var responseData = BaseResponse<User>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ONE_USER_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("create", Name = "CreateUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<User>> CreateUser([FromBody] UserRequest request)
		{
			User user = _service.Register(request);

			var responseData = BaseResponse<User>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("REGISTER_USER_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}


		[HttpPost("register_admin", Name = "RegisterAdmin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<BaseResponse<User>> RegisterAdmin([FromBody] UserRequest request)
		{
			User user = _service.RegisterAdmin(request);

			var responseData = BaseResponse<User>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("REGISTER_ADMIN_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("register_user", Name = "RegisterUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<BaseResponse<User>> RegisterUser([FromBody] UserRequest request)
		{
			User user = _service.RegisterUser(request);

			var responseData = BaseResponse<User>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("REGISTER_USER_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}


		[HttpPut("{id:int}", Name = "UpdateUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<User>> UpdateUser(int id, [FromBody] UserRequest request)
		{
			var user = _service.Update(id, request);
			var responseData = BaseResponse<User>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("UPDATE_USER_SUCCESS")
				.Data(user)
				.Build();

			return Ok(responseData);
		}


		[HttpPost("delete", Name = "DeleteUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<string>>> DeleteUser([FromBody] DeleteRequest ids)
		{
			var deletedIds = _service.Delete(ids);
			var responseData = BaseResponse<List<string>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("USERS_DELETED")
				.Data(deletedIds)
				.Build();

			return Ok(responseData);
		}

	}
}
