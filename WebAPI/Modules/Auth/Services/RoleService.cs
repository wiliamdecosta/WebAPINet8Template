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
	public class RoleService
	{

		private readonly RequestValidator _validator;
		private readonly RoleRepository _repository;
		private readonly IMapper _mapper;
		private readonly FilterUtil<Role> _filterUtil;

		public RoleService(RequestValidator validator, 
			RoleRepository repository, 
			IMapper mapper, 
			FilterUtil<Role> filterUtil)
		{
			_validator = validator;
			_repository = repository;
			_mapper = mapper;
			_filterUtil = filterUtil;
		}

		public List<Role> FetchAll()
		{
			return _repository.FetchAll();
		}

		public Paginated<Role> FetchAll(SearchRequest request)
		{
			List<string> searchKeywordsColumns = new();
			searchKeywordsColumns.Add("Name");

			Paginated<Role> query = _filterUtil.GetContent(request, searchKeywordsColumns);
			return query;
		}

		public Role FetchOne(int id)
		{
			Role? user = _repository.FetchOne(id);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}
			return user;
		}

		public Role Create(RoleRequest request)
		{

			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			Role newRole = _mapper.Map<Role>(request);
			newRole.CreatedDate = DateTime.Now;
			newRole.UpdatedDate = DateTime.Now;

			return _repository.Create(newRole);
		}

		public Role Update(int id, RoleRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			Role? role = _repository.FetchOne(id);
			if (role == null)
			{
				throw new InvalidRequestValueException(null, "INVALID_ID");
			}

			_mapper.Map(request, role);
			role.UpdatedDate = DateTime.Now;

			return _repository.Update(role);
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
