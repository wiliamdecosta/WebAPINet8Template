using AutoMapper;
using DataProvider.DbRepository.Auth.Implementation;
using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Util.Validators;
using WebAPI.Modules.Auth.Models.Requests;

namespace WebAPI.Modules.Auth.Services
{
	public class EndpointRoleService
	{
		private readonly RequestValidator _validator;
		private readonly EndpointRoleRepository _repository;
		private readonly EndpointRepository _endpointRepository;
		private readonly RoleRepository _roleRepository;
		private readonly IMapper _mapper;

		public EndpointRoleService(RequestValidator validator, 
			EndpointRoleRepository repository, 
			EndpointRepository endpointRepository, 
			RoleRepository roleRepository, 
			IMapper mapper)
		{
			_validator = validator;
			_repository = repository;
			_endpointRepository = endpointRepository;
			_roleRepository = roleRepository;
			_mapper = mapper;
		}

		public List<EndpointRole> FetchAll()
		{
			return _repository.FetchAll();
		}

		public List<EndpointRole> FetchAllByEndpointId(int endpointId)
		{
			EndpointPath? endpoint = _endpointRepository.FetchOne(endpointId);
			if (endpoint == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}

			return _repository.FetchAllByEndpointId(endpointId);
		}

		public List<EndpointRole> FetchAllByRoleId(int roleId)
		{

			Role? role = _roleRepository.FetchOne(roleId);
			if (role == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}

			return _repository.FetchAllByRoleId(roleId);
		}


		public EndpointRole FetchOne(int id)
		{
			EndpointRole? endpointRole = _repository.FetchOne(id);
			if (endpointRole == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}
			return endpointRole;
		}


		public EndpointRole Create(EndpointRoleRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			EndpointRole endpointRole = _mapper.Map<EndpointRole>(request);
			endpointRole.CreatedDate = DateTime.Now;
			endpointRole.UpdatedDate = DateTime.Now;

			return _repository.Create(endpointRole);
		}

		public EndpointRole Update(int id, EndpointRoleRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			EndpointRole? endpointRole = _repository.FetchOne(id);
			if (endpointRole == null)
			{
				throw new InvalidRequestValueException(null, "INVALID_ID");
			}

			_mapper.Map<EndpointRoleRequest, EndpointRole>(request, endpointRole);
			endpointRole.UpdatedDate = DateTime.Now;

			return _repository.Update(endpointRole);
			
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
