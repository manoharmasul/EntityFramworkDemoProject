using EntityFramworkDemoProject.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkDemoProject.Models
{

    [Table("tblProduct")]
    public class Products:BaseModel
    {
        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public long AvailableQty { get; set; }

    }
    public class ProductsInsert 
    {
        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public long AvailableQty { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }
    public class UpdateProduct
    {

        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ModifiedBy { get; set; }
        public long AvailableQty { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
