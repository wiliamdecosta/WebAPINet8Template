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
	[Route("api/v1/endpoint")]
	public class EndpointController : ControllerBase
	{

		private readonly EndpointService _service;

		public EndpointController(EndpointService service)
		{
			_service = service;
		}

		[HttpPost("all", Name = "GetAllEndpoints")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize]
		public ActionResult<BaseResponse<List<EndpointPath>>> GetAllEndpoints([FromBody] SearchRequest request)
		{
			Paginated<EndpointPath> paginatedItem = _service.FetchAll(request);

			var responseData = BaseResponse<List<EndpointPath>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ALL_ENDPOINT_LIST")
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

		[HttpGet("list", Name = "GetListEndpoints")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<EndpointPath>>> ListEndpoint()
		{
			List<EndpointPath> endpointPath = _service.FetchAll();

			var responseData = BaseResponse<List<EndpointPath>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_LIST_ROLE_SUCCESS")
				.Data(endpointPath)
				.Build();

			return Ok(responseData);
		}

		[HttpGet("{id:int}", Name = "GetEndpoint")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointPath>> GetEndpoint(int id)
		{
			var endpoint = _service.FetchOne(id);
			var responseData = BaseResponse<EndpointPath>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("FETCH_ONE_ENDPOINT_SUCCESS")
				.Data(endpoint)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("create", Name = "CreateEndpoint")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointPath>> CreateEndpoint([FromBody] EndpointRequest request)
		{
			EndpointPath endpoint = _service.Create(request);

			var responseData = BaseResponse<EndpointPath>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("CREATE_ENDPOINT_SUCCESS")
				.Data(endpoint)
				.Build();

			return Ok(responseData);
		}

		[HttpPut("{id:int}", Name = "UpdateEndpoint")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<EndpointPath>> UpdateRole(int id, [FromBody] EndpointRequest request)
		{
			var endpoint = _service.Update(id, request);
			var responseData = BaseResponse<EndpointPath>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("UPDATE_ENDPOINT_SUCCESS")
				.Data(endpoint)
				.Build();

			return Ok(responseData);
		}

		[HttpPost("delete", Name = "DeleteEndpoint")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		public ActionResult<BaseResponse<List<string>>> DeleteRole([FromBody] DeleteRequest ids)
		{
			var deletedIds = _service.Delete(ids);
			var responseData = BaseResponse<List<string>>.Builder()
				.Code(StatusCodes.Status200OK)
				.Message("ENDPOINTS_DELETED")
				.Data(deletedIds)
				.Build();

			return Ok(responseData);
		}

	}
}
