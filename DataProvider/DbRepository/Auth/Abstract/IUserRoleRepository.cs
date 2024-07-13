
using Entities.Auth;

namespace DataProvider.DbRepository.Auth.Abstract
{
	public interface IUserRoleRepository
	{
		List<UserRole> FetchAll();
		UserRole? FetchOne(int id);
		List<UserRole> FetchAllByUserId(int userId);
		List<UserRole> FetchAllByRoleId(int roleId);
		UserRole Create(UserRole userRole);
		UserRole? Update(UserRole userRole);
		int? Delete(int id);
	}
}
