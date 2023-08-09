using Api.IRepository;
using Api.Models;

namespace Api.Repository;

public class AddressRepository : GenericRepository<Address, Address>, IAddressRepository
{
}
