using EntityFramworkDemoProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkDemoProject.Models
{
    [Table("tblOrderDetails")]
    public class OrderDetails:BaseModel
    {
        //Id,ProductId,Qty,OrderAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
        public decimal OrderAmmount { get; set; }
        public long OrderId { get; set; }
       


    }
    public class ProductAndQty
    {
        public long ProductId { get; set; }
        public int Qty { get; set; }
    }
    public class GetOrderDetails
    {
        //Id,ProductId,Qty,OrderAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
        public decimal OrderAmmount { get; set; }
        public long OrderId { get; set; }



    }
    public class GetOrderDetailsJoin
    {
        //Id,ProductId,Qty,OrderAmmount,CreatedBy,CreateDate,ModifiedBy,ModifiedDate      
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal OrderAmmount { get; set; }
    }

}
