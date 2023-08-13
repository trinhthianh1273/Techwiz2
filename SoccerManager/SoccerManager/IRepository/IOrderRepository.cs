using SoccerManager.DTO.Response;

namespace SoccerManager.IRepository;

public interface IOrderRepository
{
    List<OrderRespone> GetAllResponse();
    OrderRespone GetResponseById(int? id);
}
