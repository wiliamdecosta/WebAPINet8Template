using Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.InMemoryRepository.Auth.Abstract
{
	public interface IRoleRepository
	{
		List<Role> FetchAll();
	}
}
