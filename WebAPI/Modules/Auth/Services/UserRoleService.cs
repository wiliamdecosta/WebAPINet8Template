

using AutoMapper;
using DataProvider.DbRepository.Auth.Implementation;
using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Util.Validators;
using WebAPI.Modules.Auth.Models.Requests;

namespace WebAPI.Modules.Auth.Services
{
	public class UserRoleService
	{
		private readonly RequestValidator _validator;
		private readonly UserRoleRepository _repository;
		private readonly RoleRepository _roleRepository;
		private readonly UserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserRoleService(RequestValidator validator, 
			UserRoleRepository repository, 
			UserRepository userRepository, 
			RoleRepository roleRepository,
			IMapper mapper)
		{
			_validator = validator;
			_repository = repository;
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_mapper = mapper;
		}

		public List<UserRole> FetchAll()
		{
			return _repository.FetchAll();
		}

		public List<UserRole> FetchAllByRoleId(int roleId)
		{

			Role? role = _roleRepository.FetchOne(roleId);
			if (role == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}

			return _repository.FetchAllByRoleId(roleId);
		}

		public List<UserRole> FetchAllByUserId(int userId)
		{

			User? user = _userRepository.FetchOne(userId);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}

			return _repository.FetchAllByUserId(userId);
		}

		public UserRole FetchOne(int id)
		{
			UserRole? userRole = _repository.FetchOne(id);
			if (userRole == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}
			return userRole;
		}

		public UserRole Create(UserRoleRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			UserRole userRole = _mapper.Map<UserRole>(request);
			userRole.CreatedDate = DateTime.Now;
			userRole.UpdatedDate = DateTime.Now;

			return _repository.Create(userRole);
		}

		public UserRole Update(int id, UserRoleRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			UserRole? userRole = _repository.FetchOne(id);
			if (userRole == null)
			{
				throw new InvalidRequestValueException(null, "INVALID_ID");
			}

			_mapper.Map(request, userRole);
			userRole.UpdatedDate = DateTime.Now;

			return _repository.Update(userRole);
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
