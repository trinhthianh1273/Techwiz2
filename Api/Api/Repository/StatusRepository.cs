using Api.IRepository;
using Api.Models;

namespace Api.Repository;

public class StatusRepository : GenericRepository<Status, Status>, IStatusRepository
{
}
