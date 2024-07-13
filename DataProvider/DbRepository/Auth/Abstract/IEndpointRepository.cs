using Entities.Auth;

namespace DataProvider.DbRepository.Auth.Abstract
{
	public interface IEndpointRepository
	{
		List<EndpointPath> FetchAll();
		EndpointPath? FetchOne(int id);
		EndpointPath Create(EndpointPath endpointpath);
		EndpointPath Update(EndpointPath endpointpath);
		int? Delete(int id);
	}
}
