using Entities.Auth;

namespace DataProvider.DbRepository.Auth.Abstract
{
	public interface IRoleRepository
	{
		List<Role> FetchAll();
		Role? FetchOne(int id);
		Role? FetchOneByName(string name);
		Role Create(Role role);
		Role Update(Role role);
		int? Delete(int id);
	}
}
