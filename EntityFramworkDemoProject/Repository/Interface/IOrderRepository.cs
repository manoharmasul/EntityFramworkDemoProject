using EntityFramworkDemoProject.Models;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<long> PlaceNewOrder(OrderInsert order);
        Task<decimal> TotalAmmountUpDate(long ordId,long custmerId, long CreatedBy, List<ProductAndQty> prodanqty);
        Task<long> UpdateOrder(Order order);
        Task<long> DeleteOrder(DeleteObj deleteObj);
        Task<List<Order>> GetAllOrder();
      //  Task<List<Order>> GetAllOrderPaginations(int pageNumber,int pageSize);
        Task<Order> GetOrderById(long id);
       
    }
}
