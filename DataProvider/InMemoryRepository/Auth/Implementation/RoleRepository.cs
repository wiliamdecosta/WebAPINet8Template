
using DataProvider.InMemoryRepository.Auth.Abstract;
using Entities.Auth;

namespace DataProvider.InMemoryRepository.Auth.Implementation
{
	public class RoleRepository : IRoleRepository
	{
		//it's just for example for in memory repo
		public static List<Role> Roles { get; } = new List<Role>
		{
			new Role { Id = 1, Name = "Admin" },
			new Role { Id = 2, Name = "User" },
			new Role { Id = 3, Name = "Guest" }
		};


		public List<Role> FetchAll()
		{
			return Roles;
		}

	}
}
