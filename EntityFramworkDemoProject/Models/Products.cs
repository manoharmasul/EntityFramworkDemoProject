using EntityFramworkDemoProject.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkDemoProject.Models
{

    [Table("tblProduct")]
    public class Products : BaseModel
    {
        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public long AvailableQty { get; set; }
        public long ProductType { get; set; }

    }
    public class ProductsInsert
    {
        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public long AvailableQty { get; set; }
        public long CreatedBy { get; set; }
        public long ProductType { get; set; }
      
       


    }

    public class GetProducts
    {
        //Id,ProductName,Price
        public long Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public long AvailableQty { get; set; }
        public long? ProductType { get; set; }

        public long CreatedBy { get; set; }
 
        public long? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }


    }
}
