using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;


namespace Higgs.Mbale.BAL.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(long orderId);
        long SaveOrder(Order order, string userId);
        void MarkAsDeleted(long orderId, string userId);
        IEnumerable<Order> GetAllOrdersForAParticularBranch(long branchId);
        IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId);
        void UpdateOrderWithCompletedStatus(long orderId, long statusId, string userId);
        IEnumerable<Order> GetAllCompletedOrdersForAParticularCustomer(string customerId, long statusId);
        IEnumerable<Order> GetAllOpenOrdersForAParticularCustomer(string customerId, long statusId);
        void UpdateOrderWithInProgressStatus(long orderId, long statusId, string userId);
        IEnumerable<Order> MapEFToModel(IEnumerable<EF.Models.Order> data);
      
        
    }
}
