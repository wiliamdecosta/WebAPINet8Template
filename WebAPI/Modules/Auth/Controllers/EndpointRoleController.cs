using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Modules.Auth.Models.Requests;
using WebAPI.Modules.Auth.Services;

namespace WebAPI.Modules.Auth.Controllers
{
	[Route("api/v1/endpointrole")]
	public class EndpointRoleController : ControllerBase
	{
		private readonly EndpointRoleService _service;

		public EndpointRoleController(EndpointRoleService service)
		{
			_service = service;
		}

		[HttpGet("list", Name = "GetListEndpointRoles")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<EndpointRole>>> ListEndpointRole()
		{
			List<EndpointRole> endpointRole = _service.FetchAll();

			var responseData = BaseResponse<List<EndpointRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ENDPOINT_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("{id:int}", Name = "GetEndpointRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointRole>> GetEndpointRole(int id)
		{
			var endpointRole = _service.FetchOne(id);
			var responseData = BaseResponse<EndpointRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ONE_ENDPOINT_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("list-by-role-id/{id:int}", Name = "GetEndpointRoleByRoleId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<EndpointRole>>> GetEndpointRoleByRoleId(int id)
		{
			var endpointRole = _service.FetchAllByRoleId(id);
			var responseData = BaseResponse<List<EndpointRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ENDPOINT_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("list-by-endpoint-id/{id:int}", Name = "GetEndpointRoleByEndpointId")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<EndpointRole>>> GetEndpointRoleByEndpointId(int id)
		{
			var endpointRole = _service.FetchAllByEndpointId(id);
			var responseData = BaseResponse<List<EndpointRole>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ENDPOINT_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("create", Name = "CreateEndpointRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointRole>> CreateEndpointRole([FromBody] EndpointRoleRequest request)
		{
			EndpointRole endpointRole = _service.Create(request);

			var responseData = BaseResponse<EndpointRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("CREATE_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}

		[HttpPut("{id:int}", Name = "UpdateEndpointRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointRole>> UpdateEndpointRole(int id, [FromBody] EndpointRoleRequest request)
		{
			var endpointRole = _service.Update(id, request);
			var responseData = BaseResponse<EndpointRole>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("UPDATE_ENDPOINT_ROLE_SUCCESS")
				.Data(endpointRole)
				.Build();

			return Ok(responseData);
		}


		[HttpPost("delete", Name = "DeleteEndpointRole")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<string>>> DeleteRole([FromBody] DeleteRequest ids)
		{
			var deletedIds = _service.Delete(ids);
			var responseData = BaseResponse<List<string>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("ENDPOINT_ROLES_DELETED")
				.Data(deletedIds)
				.Build();

			return Ok(responseData);
		}

	}
}
