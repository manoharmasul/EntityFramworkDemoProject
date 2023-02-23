using EntityFramworkDemoProject.Context;
using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EntityFramworkDemoProject.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly MyContext _myContext;

        public OrderRepository(MyContext context)
        {
            _myContext = context;   
        }

        public async Task<long> DeleteOrder(BaseModel.DeleteObj deleteObj)
        {
            var result = 0;
            List<OrderDetails> odlist = new List<OrderDetails>();
            var ord=await _myContext.Order.FindAsync(deleteObj.Id);
            if(ord != null)
            {
                ord.IsDeleted = true;
                ord.ModifiedBy=deleteObj.ModifiedBy;
                ord.ModifiedDate=DateTime.Now;

                var upOrder = _myContext.Order.Update(ord);
                 result =  _myContext.SaveChanges();


                var orddetails = from s in _myContext.OrderDetails

                                 where s.OrderId == ord.Id
                                 select s;
                var ordlist11 = await orddetails.ToListAsync();

                odlist = ordlist11.ToList();
                foreach (var details in ordlist11)
                {
                    details.ModifiedBy = deleteObj.ModifiedBy;
                    details.ModifiedDate=deleteObj.ModifiedDate;
                    details.IsDeleted = true;
                    var up = _myContext.OrderDetails.Update(details);

                    var ret = _myContext.SaveChanges();
                }

            }
            return result;
        }

        public async Task<List<Order>> GetAllOrder()
        {
            List<Order> ordlist=new List<Order>();
            

             ordlist = await _myContext.Order.ToListAsync();

            var orders= from s in _myContext.Order

                                       where s.IsDeleted == false 
                                       select s;
            var ordlist11 = await orders.ToListAsync();

            ordlist = ordlist11.ToList();


            //List<OrderDetails> orddetail = new List<OrderDetails>();





            foreach (var item in ordlist)
             {

                 var orddetails= from s in _myContext.OrderDetails

                 where s.OrderId == item.Id
                                       select s;
                 var ordlist1= await orddetails.ToListAsync();

                 item.orddetails = ordlist1.ToList();


                 item.orddetails =  await _myContext.OrderDetails
                  .Where(s => s.OrderId == item.Id)
                  .ToListAsync<OrderDetails>();


                //item.orddetails = ordlist11.ToLists();
            }
            return ordlist.ToList();
        }

        public async Task<List<GetOrdersJoin>> GetAllOrderJoinUser(string OrderStatus)
        {
            var query = from or in _myContext.Order join od in _myContext.tblUser

                        on or.CustomerId equals od.Id

                        where or.OrderStatus == OrderStatus

                        orderby or.Id descending select new GetOrdersJoin

                        {
                            Id = or.Id,
                            UserName = od.UserName,
                            MobileNo = od.MobileNo,
                            BillingAddress = or.BillingAddress,
                            ShippingAddress = or.ShippingAddress,
                            OrderStatus = or.OrderStatus,
                            OrderDate = or.OrderDate,
                            TotalAmmount = or.TotalAmmount,
                            DeliveredDate= or.DeliveredDate   

                        };
            var result=await query.ToListAsync();

            foreach(var item in result)
            {
                var queryDetails = from od in _myContext.OrderDetails
                            join
                          p in _myContext.Products on od.ProductId equals p.Id

                            where od.OrderId == item.Id

                            select new GetOrderDetailsJoin
                            {
                                ProductName = p.ProductName,
                                Qty = od.Qty,
                                OrderAmmount = od.OrderAmmount,
                            };
                var orddetails=await queryDetails.ToListAsync();

                item.orderdetails=orddetails;


            }

            return result;


        }

        public async Task<List<GetOrdersJoin>> GetOrderByCustIdAndStatusDate(long custId, string? orderstatus, DateTime? date)
        {

            List<GetOrdersJoin> order = new List<GetOrdersJoin>();

            if (orderstatus == null && date != null)
            {
                var query = from o in _myContext.Order
                            join ur in _myContext.tblUser on o.CustomerId equals ur.Id
                            where o.CustomerId == custId && (o.OrderDate.Date == date)
                            orderby o.Id descending

                            select new GetOrdersJoin
                            {
                                Id = o.Id,
                                UserName = ur.UserName,
                                MobileNo = ur.MobileNo,
                                BillingAddress = o.BillingAddress,

                                ShippingAddress = o.ShippingAddress,
                                OrderStatus = o.OrderStatus,
                                OrderDate = o.OrderDate,

                                TotalAmmount = o.TotalAmmount

                            };
                 order = await query.ToListAsync();

            }
            else if (date == null && orderstatus != null)
            {
                var query = from o in _myContext.Order
                            join ur in _myContext.tblUser on o.CustomerId equals ur.Id

                            where o.CustomerId == custId && (o.OrderStatus == orderstatus)

                            orderby o.Id descending

                            select new GetOrdersJoin
                            {
                                Id = o.Id,
                                UserName = ur.UserName,
                                MobileNo = ur.MobileNo,
                                BillingAddress = o.BillingAddress,

                                ShippingAddress = o.ShippingAddress,
                                OrderStatus = o.OrderStatus,
                                OrderDate = o.OrderDate,

                                TotalAmmount = o.TotalAmmount

                            };
                 order = await query.ToListAsync();

            }
            else if (date == null && orderstatus == null)
            {
                var query = from o in _myContext.Order join ur in _myContext.tblUser on o.CustomerId equals ur.Id
                            where o.CustomerId == custId 
                            orderby o.Id descending
                            select new GetOrdersJoin
                            {
                                Id = o.Id,
                                UserName = ur.UserName,
                                MobileNo = ur.MobileNo,
                                BillingAddress = o.BillingAddress,

                                ShippingAddress = o.ShippingAddress,
                                OrderStatus = o.OrderStatus,
                                OrderDate = o.OrderDate,

                                TotalAmmount = o.TotalAmmount

                            };
                 order = await query.ToListAsync();
            }
            else
            {
                var query = from o in _myContext.Order
                            join ur in _myContext.tblUser on o.CustomerId equals ur.Id
                            where
                            o.CustomerId == custId && (o.OrderStatus == orderstatus) && (o.OrderDate.Date == date)
                            orderby o.Id descending

                            select new GetOrdersJoin
                            {

                                Id = o.Id,

                                UserName = ur.UserName,

                                MobileNo = ur.MobileNo,

                                BillingAddress = o.BillingAddress,

                                ShippingAddress = o.ShippingAddress,

                                OrderStatus = o.OrderStatus,

                                OrderDate = o.OrderDate,

                                TotalAmmount = o.TotalAmmount

                            };
                 order = await query.ToListAsync();
            }

           

            if (order != null)
            {
                foreach (var o in order)
                {
                    var querydetails = from od in _myContext.OrderDetails
                                       join p in _myContext.Products on od.ProductId equals (p.Id)
                                       where od.OrderId == o.Id orderby od.Id descending
                                       select new GetOrderDetailsJoin
                                       {

                                           ProductName = p.ProductName,
                                           Qty = od.Qty,
                                           OrderAmmount = od.OrderAmmount,

                                       };

                    var orddetailslist = await querydetails.ToListAsync();

                    o.orderdetails = orddetailslist;
                }
            }



            
            return order;
        }

        public async Task<Order> GetOrderById(long id)
        {
            List<OrderDetails> orderlists = new List<OrderDetails>();
            Order ord = new Order();
           
            var query = from s in _myContext.Order

                             where s.IsDeleted == false
                              where s.Id==id
                             select s;

            ord = await query.SingleOrDefaultAsync();

           
            

            var queryorddetails = from s in _myContext.OrderDetails

                        where s.IsDeleted == false
                        where s.OrderId == id
                        select s;

            orderlists=await queryorddetails.ToListAsync<OrderDetails>();

            ord.orddetails=orderlists.ToList();

            return ord;




        }

        public async Task<decimal> PlaceNewOrder(OrderInsert order)
        {
            List<ProductAndQty> prodqty=new List<ProductAndQty>();  
            TotalAmmountUpdate ammountupdate=new TotalAmmountUpdate();  


            Order ord = new Order();
            ord.CreatedBy= order.CreatedBy;
            ord.CreatedDate = DateTime.Now;
            ord.CustomerId = order.CustomerId;
            ord.BillingAddress=order.BillingAddress;
            ord.ShippingAddress=order.ShippingAddress;
            ord.ModifiedBy = 0;        
            ord.TotalAmmount = 0;
            ord.OrderDate = DateTime.Now;
            ord.OrderStatus = "Pending";


            var query=_myContext.AddAsync(ord);

            var result =await _myContext.SaveChangesAsync();

            long ordId=ord.Id;

            prodqty=order.prodqty;

            var total = await TotalAmmountUpDate(ordId, order.CustomerId,ord.CreatedBy, order.prodqty);

            var updOrd=await _myContext.Order.FindAsync(ord.Id);

            updOrd.TotalAmmount= total;

            var qeury = _myContext.Order.Update(updOrd); 

            result=await _myContext.SaveChangesAsync();


            return total;  
        }

        public async Task<decimal> TotalAmmountUpDate(long ordId,long customerId,long CreatedBy, List<ProductAndQty> prodanqty)
        {
            decimal totalammounts = 0;

            foreach (var prod in prodanqty)
            {
               

                OrderDetails orddetails = new OrderDetails();
                orddetails.ProductId=prod.ProductId;
                orddetails.OrderId=ordId;
                orddetails.Qty=prod.Qty;
                var product=await _myContext.Products.FindAsync(prod.ProductId);
                product.AvailableQty-=orddetails.Qty;
                var queryup = _myContext.Products.Update(product);
                var updateprod=await _myContext.SaveChangesAsync(); 

                orddetails.OrderAmmount = product.Price * orddetails.Qty;
                orddetails.CreatedDate = DateTime.Now;             
                orddetails.ModifiedBy = 0;
                orddetails.CreatedBy= CreatedBy;
                orddetails.IsDeleted = false;
                totalammounts = orddetails.OrderAmmount + totalammounts;
                var query =  _myContext.AddAsync(orddetails);
                var result = await _myContext.SaveChangesAsync();
            }
            return totalammounts;
        }

        public Task<long> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<long> UpdateOrderStatus(UpdateOrderStatus updatestatus)
        {
            long result = 0;
            Order ord = new Order();

            var queryorder = from o in _myContext.Order
                             where o.Id == updatestatus.Id
                             select o;
             ord = await queryorder.FirstOrDefaultAsync();

           

            if (updatestatus.OrderStatus =="Delivered")
            {

                ord.DeliveredDate = DateTime.Now;

                ord.OrderStatus = updatestatus.OrderStatus;

                ord.ModifiedBy = updatestatus.ModifiedBy;

                ord.ModifiedDate = DateTime.Now;

                var upoder = _myContext.Order.Update(ord);

               result = await _myContext.SaveChangesAsync();

               return result;

            }
            else
            {


                ord.ModifiedBy = updatestatus.ModifiedBy;
                ord.ModifiedDate = DateTime.Now;
                ord.OrderStatus=updatestatus.OrderStatus;   
                var upoder = _myContext.Order.Update(ord);

                result = await _myContext.SaveChangesAsync();

                return result;

            }
            

            
        }
    }
}
