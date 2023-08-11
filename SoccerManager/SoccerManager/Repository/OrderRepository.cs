using Microsoft.EntityFrameworkCore;
using SoccerManager.DTO.Response;
using SoccerManager.IRepository;
using SoccerManager.Models;

namespace SoccerManager.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly SoccerContext _context;

    public OrderRepository(SoccerContext context)
    {
        _context = context;
    }

    public List<OrderRespone> GetAllResponse()
    {
        List<OrderRespone> results = new List<OrderRespone>();
        var soccerContext = _context.Orders.Include(o => o.Address).Include(o => o.Customer).Include(o => o.Employee).Include(o => o.PaymentMethod).Include(o => o.Status);
        foreach (var order in soccerContext)
        {
            order.OrderContent = _context.OrderContent.Where(o => o.OrderId == order.OrderId).Include(o => o.Product).ToList();
            for(int i = 0; i < order.OrderContent.Count();  i++)
            {
                order.OrderContent.ElementAt(i).Product.ProductImage = _context.ProductImage.Where(p => p.ProductId == order.OrderContent.ElementAt(i).Product.ProductId).ToList();
            }
            results.Add(OrderRespone.ConvertToOrderResponse(order));
        }
        return results;
    }

    public OrderRespone GetResponseById(int? id)
    {
        return GetAllResponse().Where(o => o.OrderId == id).Single();
    }
}
