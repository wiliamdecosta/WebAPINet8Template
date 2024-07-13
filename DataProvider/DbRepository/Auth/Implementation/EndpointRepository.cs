using DataProvider.DbRepository.Auth.Abstract;
using Entities.Auth;
using Infrastructure.Db;

namespace DataProvider.DbRepository.Auth.Implementation
{
	public class EndpointRepository : IEndpointRepository
	{
		private readonly ApplicationDbContext _db;
		

		public EndpointRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public List<EndpointPath> FetchAll()
		{
			return _db.Endpoints.ToList();
		}

		public EndpointPath? FetchOne(int id)
		{
			return _db.Endpoints.FirstOrDefault(x => x.Id == id);
		}

		public EndpointPath Create(EndpointPath newPath)
		{
			
			var createdPath = _db.Endpoints.Add(newPath);
			_db.SaveChanges();

			return createdPath.Entity;
		}

		public EndpointPath Update(EndpointPath endpoint)
		{
			
			_db.Endpoints.Update(endpoint);
			_db.SaveChanges();
			return endpoint;
		}

		public int? Delete(int id)
		{
			var endpoint = _db.Endpoints.FirstOrDefault(x => x.Id == id);
			if (endpoint == null) return null;

			var endpointRoles = _db.EndpointRoles.Where(y => y.EndpointId == endpoint.Id).First();
			if (endpointRoles != null)
			{
				_db.EndpointRoles.Remove(endpointRoles);
			}

			_db.Endpoints.Remove(endpoint);
			_db.SaveChanges();
			return id;
		}

	}
}
