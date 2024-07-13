using DataProvider.DbRepository.Auth.Abstract;
using Entities.Auth;
using Infrastructure.Db;

namespace DataProvider.DbRepository.Auth.Implementation
{
	public class UserRoleRepository : IUserRoleRepository
	{

		private readonly ApplicationDbContext _db;

		public UserRoleRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public List<UserRole> FetchAll()
		{
			return _db.UserRoles.ToList();
		}

		public List<UserRole> FetchAllByRoleId(int roleId)
		{
			return _db.UserRoles.Where((e) => e.RoleId == roleId).ToList();
		}

		public List<UserRole> FetchAllByUserId(int userId)
		{
			return _db.UserRoles.Where((e) => e.UserId == userId).ToList();
		}

		public UserRole? FetchOne(int id)
		{
			return _db.UserRoles.FirstOrDefault(x => x.Id == id);
		}

		public UserRole Create(UserRole userRole)
		{
			var createdUserRole = _db.UserRoles.Add(userRole);
			_db.SaveChanges();

			return createdUserRole.Entity;
		}

		public UserRole Update(UserRole userRole)
		{
			_db.UserRoles.Update(userRole);
			_db.SaveChanges();
			return userRole;
		}

		public int? Delete(int id)
		{
			var userRole = _db.UserRoles.FirstOrDefault(x => x.Id == id);
			if (userRole == null) return null;

			_db.UserRoles.Remove(userRole);
			_db.SaveChanges();
			return id;
		}

	}
}
