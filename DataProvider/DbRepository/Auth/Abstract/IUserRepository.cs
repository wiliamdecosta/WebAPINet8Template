
using Entities.Auth;

namespace DataProvider.DbRepository.Auth.Abstract
{
    public interface IUserRepository
    {
        List<User> FetchAll();
        User? FetchOne(int id);
        User? FetchByEmail(string email);
		User Create(User user);
        User Update(User user);
        int? Delete(int id);
	}
}
