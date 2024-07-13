

using AutoMapper;
using DataProvider.DbRepository.Auth.Implementation;
using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Requests.Filters;
using Infrastructure.Util;
using Infrastructure.Util.Validators;
using WebAPI.Modules.Auth.Models.Requests;

namespace WebAPI.Modules.Auth.Services
{
	public class EndpointService
	{
		private readonly RequestValidator _validator;
		private readonly EndpointRepository _repository;
		private readonly IMapper _mapper;
		private readonly FilterUtil<EndpointPath> _filterUtil;

		public EndpointService(RequestValidator validator, 
			EndpointRepository repository,
			IMapper mapper,
			FilterUtil<EndpointPath> filterUtil)
		{
			_validator = validator;
			_repository = repository;
			_mapper = mapper;
			_filterUtil = filterUtil;
		}

		public List<EndpointPath> FetchAll()
		{
			return _repository.FetchAll();
		}

		public Paginated<EndpointPath> FetchAll(SearchRequest request)
		{
			List<string> searchKeywordsColumns = new();
			searchKeywordsColumns.Add("Name");

			Paginated<EndpointPath> query = _filterUtil.GetContent(request, searchKeywordsColumns);
			return query;
		}

		public EndpointPath FetchOne(int id)
		{
			EndpointPath? user = _repository.FetchOne(id);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}
			return user;
		}

		public EndpointPath Create(EndpointRequest request)
		{

			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			EndpointPath newPath = _mapper.Map<EndpointPath>(request);
			newPath.CreatedDate = DateTime.Now;
			newPath.UpdatedDate = DateTime.Now;


			return _repository.Create(newPath);
		}

		public EndpointPath Update(int id, EndpointRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			EndpointPath? endpoint = _repository.FetchOne(id);
			if (endpoint == null)
			{
				throw new InvalidRequestValueException(null, "INVALID_ID");
			}


			_mapper.Map<EndpointRequest, EndpointPath>(request, endpoint);
			endpoint.UpdatedDate = DateTime.Now;

			return _repository.Update(endpoint);
		}

		public List<string> Delete(DeleteRequest request)
		{
			List<string> deletedIds = [];
			foreach (string userId in request.Ids)
			{
				int? deletedId = _repository.Delete(int.Parse(userId));
				if (deletedId != null) deletedIds.Add(userId);
			}
			return deletedIds;
		}

	}
}
