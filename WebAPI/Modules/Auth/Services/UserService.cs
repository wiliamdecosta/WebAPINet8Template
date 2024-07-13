

using AutoMapper;
using DataProvider.DbRepository.Auth.Implementation;
using Entities.Auth;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Requests.Filters;
using Infrastructure.Util;
using Infrastructure.Util.Validators;
using WebAPI.Modules.Auth.Models.Requests;
using WebAPI.Modules.Auth.Models.Responses;

namespace WebAPI.Modules.Auth.Services
{
	public class UserService
	{
		private readonly RequestValidator _validator;
		private readonly UserRepository _repository;
		private readonly RoleRepository _roleRepository;
		private readonly UserRoleRepository _userRoleRepository;
		private readonly IMapper _mapper;
		private readonly FilterUtil<User> _filterUtil;

		public UserService(RequestValidator validator, 
			UserRepository repository, 
			RoleRepository roleRepository,
			UserRoleRepository userRoleRepository,
			IMapper mapper, 
			FilterUtil<User> filterUtil)
		{
			_validator = validator;
			_repository = repository;
			_roleRepository = roleRepository;
			_mapper = mapper;
			_filterUtil = filterUtil;
			_userRoleRepository = userRoleRepository;
		}

		public List<User> FetchAll()
		{
			return _repository.FetchAll();
		}

		public Paginated<User> FetchAll(SearchRequest request)
		{
			List<string> searchKeywordsColumns = new();
			searchKeywordsColumns.Add("Name");
			searchKeywordsColumns.Add("Email");

			Paginated<User> query = _filterUtil.GetContent(request, searchKeywordsColumns);
			return query;
		}

		public User FetchOne(int id)
		{
			User? user = _repository.FetchOne(id);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "ID_NOT_FOUND");
			}
			return user;
		}

		public User? FetchByEmail(string email)
		{
			User? user = _repository.FetchByEmail(email);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "EMAIL_NOT_FOUND");
			}
			return user;
		}

		public User Register(UserRequest request)
		{

			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			User newUser = _mapper.Map<User>(request);
			newUser.CreatedDate = DateTime.Now;
			newUser.UpdatedDate = DateTime.Now;
			newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

			User user = _repository.Create(newUser);
			return user;
		}

		public User RegisterAdmin(UserRequest request)
		{

			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			User newUser = _mapper.Map<User>(request);
			newUser.CreatedDate = DateTime.Now;
			newUser.UpdatedDate = DateTime.Now;
			newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

			var createdUser = _repository.Create(newUser);

			var role = _roleRepository.FetchOneByName("Admin");
			if (role != null)
			{
				var newUserRole = new UserRole()
				{
					UserId = createdUser.Id,
					RoleId = role.Id
				};
				_userRoleRepository.Create(newUserRole);
			}

			return createdUser;
		}

		public User RegisterUser(UserRequest request)
		{

			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			User newUser = _mapper.Map<User>(request);
			newUser.CreatedDate = DateTime.Now;
			newUser.UpdatedDate = DateTime.Now;
			newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

			var createdUser = _repository.Create(newUser);

			var role = _roleRepository.FetchOneByName("Operator");
			if (role != null)
			{
				var newUserRole = new UserRole()
				{
					UserId = createdUser.Id,
					RoleId = role.Id
				};
				_userRoleRepository.Create(newUserRole);
			}

			return createdUser;
		}

		public User Update(int id, UserRequest request)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			User? user = _repository.FetchOne(id);
			if (user == null)
			{
				throw new InvalidRequestValueException(null, "INVALID_ID");
			}

			_mapper.Map(request, user);
			user.UpdatedDate = DateTime.Now;
			user = _repository.Update(user);
			return user;
		}

		public List<string> Delete(DeleteRequest request)
		{
			List<string> deletedIds = [];
			foreach (string userId in request.Ids)
			{
				int? deletedId = _repository.Delete(int.Parse(userId));
				if(deletedId != null) deletedIds.Add(userId);
			}
			return deletedIds;
		}

		public User UpdateRefreshToken(User user)
		{
			User updatedUser = _repository.Update(user);
			return updatedUser;
		}

		public RefreshTokenResponse? GetNewToken(RefreshTokenRequest request, IConfiguration configuration)
		{
			if (!_validator.Validate(request))
			{
				throw new InvalidRequestValueException(_validator.Errors);
			}

			var principal = JwtUtil.GetPrincipalFromExpiredToken(request.Token, configuration);
			if (principal == null) return null;

			var userIdClaim = principal.FindFirst("UserId");
			if (userIdClaim == null) return null;

			var userId = userIdClaim.Value;
			User user = FetchOne(int.Parse(userId));

			if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
			{
				return null;
			}

			var newToken = JwtUtil.GenerateJwtToken(user, configuration);
			var newRefreshToken = JwtUtil.GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			user.RefreshTokenExpiryTime = DateTime.UtcNow;
			User updateRefreshToken = UpdateRefreshToken(user);

			return new RefreshTokenResponse()
			{
				Token = newToken,
				RefreshToken = updateRefreshToken.RefreshToken!,
			};

		}
	}
}
