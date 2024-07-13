using DataProvider.DbRepository.Auth.Abstract;
using Entities.Auth;
using Infrastructure.Db;

namespace DataProvider.DbRepository.Auth.Implementation
{
	public class EndpointRoleRepository : IEndpointRoleRepository
	{

		private readonly ApplicationDbContext _db;

		public EndpointRoleRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public List<EndpointRole> FetchAll()
		{
			return _db.EndpointRoles.ToList();
		}

		public List<EndpointRole> FetchAllByEndpointId(int endpointId)
		{
			return _db.EndpointRoles.Where((e) => e.EndpointId == endpointId).ToList();
		}

		public List<EndpointRole> FetchAllByRoleId(int roleId)
		{
			return _db.EndpointRoles.Where((e) => e.RoleId == roleId).ToList();
		}


		public EndpointRole? FetchOne(int id)
		{
			return _db.EndpointRoles.FirstOrDefault(x => x.Id == id);
		}

		public EndpointRole Create(EndpointRole endpointRole)
		{
			
			var createdEndpointRole = _db.EndpointRoles.Add(endpointRole);
			_db.SaveChanges();

			return createdEndpointRole.Entity;
		}


		public EndpointRole Update(EndpointRole endpointRole)
		{
			
			_db.EndpointRoles.Update(endpointRole);
			_db.SaveChanges();
			return endpointRole;
		}

		public int? Delete(int id)
		{
			var endpointRole = _db.EndpointRoles.FirstOrDefault(x => x.Id == id);
			if (endpointRole == null) return null;

			_db.EndpointRoles.Remove(endpointRole);
			_db.SaveChanges();
			return id;
		}

	}
}
