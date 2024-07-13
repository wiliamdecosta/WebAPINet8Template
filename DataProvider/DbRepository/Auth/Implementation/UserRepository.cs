using DataProvider.DbRepository.Auth.Abstract;
using Entities.Auth;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;


namespace DataProvider.DbRepository.Auth.Implementation
{
	public class UserRepository : IUserRepository
	{

		private readonly ApplicationDbContext _db;

		public UserRepository(ApplicationDbContext db)
		{
			_db = db;	
		}

		public List<User> FetchAll()
		{
			var result = _db.Users.Include(obj => obj.UserRole).ToList();
			var output = result;
			return result;
		}

		public User? FetchOne(int id)
		{
			return _db.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
		}

		public User? FetchByEmail(string email)
		{
			return _db.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
		}

		public User Create(User newUser)
		{
			var createdUser = _db.Users.Add(newUser);
			_db.SaveChanges();

			return createdUser.Entity;
		}

		public User Update(User user)
		{
			_db.Users.Update(user);
			_db.SaveChanges();
			return user;
		}

		public int? Delete(int id)
		{
			var user = _db.Users.FirstOrDefault(x => x.Id == id);
			if (user == null) return null;

			var userRole = _db.UserRoles.Where(y => y.UserId == user.Id).First();
			if(userRole != null)
			{
				_db.UserRoles.Remove(userRole);
			}

			_db.Users.Remove(user);
			_db.SaveChanges();
			return id;
		}

	}
}
