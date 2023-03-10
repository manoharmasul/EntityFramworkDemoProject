using EntityFramworkDemoProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkDemoProject.Models
{
    [Table("tblOrder")]
    public class Order:BaseModel
    {
        //Id,CustomerId,BillingAddress,ShippingAddress,TotalAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,Is
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmmount { get; set; }
        public List<OrderDetails> orddetails { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }


    }

    public class OrderInsert 
    {
        //Id,CustomerId,BillingAddress,ShippingAddress,TotalAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,Is
        public long CustomerId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long CreatedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public List<ProductAndQty> prodqty { get; set; }
    }
    public class UpdateOrderStatus
    {
        //Id,CustomerId,BillingAddress,ShippingAddress,TotalAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,Is
        public long Id { get; set; }
        public string OrderStatus { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class TotalAmmountUpdate
    {
        public long Id { get; set; }    
        public decimal TotalAmmount { get; set; }
    }
    public class GetOrder 
    {
        //Id,CustomerId,BillingAddress,ShippingAddress,TotalAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,Is
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmmount { get; set; }
        public List<GetOrderDetails> orddetails { get; set; }
    }
    public class GetOrdersJoin
    {
        //Id,CustomerId,BillingAddress,ShippingAddress,TotalAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate,Is
        public long Id { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    
       
        public List<GetOrderDetailsJoin> orderdetails { get; set; }

        public string OrderStatus { get; set; }

        public decimal TotalAmmount { get; set; }


    }

}
