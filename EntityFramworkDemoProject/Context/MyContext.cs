using EntityFramworkDemoProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }



        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


    }
     
}