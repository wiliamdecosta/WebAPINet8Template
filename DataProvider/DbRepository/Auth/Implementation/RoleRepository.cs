using DataProvider.DbRepository.Auth.Abstract;
using Entities.Auth;
using Infrastructure.Db;

namespace DataProvider.DbRepository.Auth.Implementation
{
	public class RoleRepository : IRoleRepository
	{
		private readonly ApplicationDbContext _db;
		

		public RoleRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public List<Role> FetchAll()
		{
			return _db.Roles.ToList();
		}

		public Role? FetchOne(int id)
		{
			return _db.Roles.FirstOrDefault(x => x.Id == id);
		}

		public Role? FetchOneByName(string name)
		{
			return _db.Roles.FirstOrDefault(x => x.Name == name);
		}

		public Role Create(Role newRole)
		{
			var createdRole = _db.Roles.Add(newRole);
			_db.SaveChanges();

			return createdRole.Entity;
		}

		public Role Update(Role role)
		{
			_db.Roles.Update(role);
			_db.SaveChanges();
			return role;
		}

		public int? Delete(int id)
		{
			var role = _db.Roles.FirstOrDefault(x => x.Id == id);
			if (role == null) return null;

			var userRole = _db.UserRoles.Where(y => y.RoleId == role.Id).First();
			if (userRole != null)
			{
				_db.UserRoles.Remove(userRole);
			}

			var endpointRoles = _db.EndpointRoles.Where(y => y.RoleId == role.Id).First();
			if (endpointRoles != null)
			{
				_db.EndpointRoles.Remove(endpointRoles);
			}

			_db.Roles.Remove(role);
			_db.SaveChanges();
			return id;
		}

	}
}
