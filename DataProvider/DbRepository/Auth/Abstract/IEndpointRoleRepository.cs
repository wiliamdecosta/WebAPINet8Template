using Entities.Auth;

namespace DataProvider.DbRepository.Auth.Abstract
{
	public interface IEndpointRoleRepository
	{

		List<EndpointRole> FetchAll();
		EndpointRole? FetchOne(int id);
		List<EndpointRole> FetchAllByRoleId(int roleId);
		List<EndpointRole> FetchAllByEndpointId(int endpointId);
		EndpointRole Create(EndpointRole endpointrole);
		EndpointRole Update(EndpointRole endpointrole);
		int? Delete(int id);
	}
}
