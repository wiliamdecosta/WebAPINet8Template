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
	[Route("api/v1/role")]
	public class RoleController : ControllerBase
	{
		private readonly RoleService _service;

		public RoleController(RoleService service)
		{
			_service = service;
		}

		[HttpPost("all", Name = "GetAllRoles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize]
		public ActionResult<BaseResponse<List<Role>>> GetAllRoles([FromBody] SearchRequest request)
		{
			Paginated<Role> paginatedItem = _service.FetchAll(request);

			var responseData = BaseResponse<List<Role>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ALL_ROLE_LIST")
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

		[HttpGet("list", Name = "GetListRoles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<Role>>> ListRole()
		{
			List<Role> role = _service.FetchAll();

			var responseData = BaseResponse<List<Role>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_LIST_ROLE_SUCCESS")
				.Data(role)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("{id:int}", Name = "GetRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<Role>> GetRole(int id)
		{
			var role = _service.FetchOne(id);
			var responseData = BaseResponse<Role>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ONE_ROLE_SUCCESS")
				.Data(role)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("create", Name = "CreateRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<User>> CreateRole([FromBody] RoleRequest request)
		{
			Role role = _service.Create(request);

			var responseData = BaseResponse<Role>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("CREATE_ROLE_SUCCESS")
				.Data(role)
				.Build();

			return Ok(responseData);
		}

		[HttpPut("{id:int}", Name = "UpdateRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<Role>> UpdateRole(int id, [FromBody] RoleRequest request)
		{
			var role = _service.Update(id, request);
			var responseData = BaseResponse<Role>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("UPDATE_ROLE_SUCCESS")
				.Data(role)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("delete", Name = "DeleteRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<string>>> DeleteRole([FromBody] DeleteRequest ids)
		{
			var deletedIds = _service.Delete(ids);
			var responseData = BaseResponse<List<string>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("ROLES_DELETED")
				.Data(deletedIds)
				.Build();

			return Ok(responseData);
		}
	}
}
