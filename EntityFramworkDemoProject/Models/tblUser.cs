using EntityFramworkDemoProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkDemoProject.Models
{
    public class tblUser:BaseModel
    {
        //Id,UserName,WaalletBalance,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public long UserType { get; set; }
   
    }
    public class UserInsertModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public long CreatedBy { get; set; }

    }
    public class UserUpdatetModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public decimal WalletBalance { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class GetUserModel
    {
        //Id,UserName,EmailId,MobileNo,WalletBalance,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public decimal WalletBalance { get; set; }
     
    }
}
