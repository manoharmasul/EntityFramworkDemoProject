using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Context;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Repository
{
    public class ProductAsyncRepository:IProductAsyncRepository
    {
        private readonly MyContext _myContext;
        public ProductAsyncRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<int> AddNewProducts(ProductsInsert prod)
        {
            Products products = new Products();

            products.Id = prod.Id;  
            products.ProductName= prod.ProductName;
            products.Price = prod.Price;
            products.AvailableQty = prod.AvailableQty;
            products.ProductType = prod.ProductType;


            products.CreatedBy = prod.CreatedBy;
            products.ModifiedBy = 0;
            products.CreatedDate = DateTime.Now;
            products.IsDeleted = false;
            var query=_myContext.AddAsync(products);
            var result = await _myContext.SaveChangesAsync();

            return result;


        }

        public async Task<int> DeleteProducts(DeleteObj deleteobj)
        {
            var prod = await _myContext.Products.FindAsync(deleteobj.Id);
            if (prod != null)
            {
                prod.IsDeleted = true;  
                prod.ModifiedBy=deleteobj.ModifiedBy;
                _myContext.Products.Update(prod);

                return await _myContext.SaveChangesAsync();
            }
            else
                return -1;

        }

        public async Task<List<GetProducts>> GetAllProducts()
        {
            var query = from p in _myContext.Products where p.IsDeleted == false
                                                                            select new GetProducts
                                                                            {
                                                                                Id = p.Id,
                                                                                ProductName = p.ProductName,    
                                                                                Price=p.Price,  
                                                                                AvailableQty=p.AvailableQty,   
                                                                               
                                                                            };
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<GetProducts> GetProductById(long id)
        {
            var query = from p in _myContext.Products
                        where p.Id == id && p.IsDeleted == false

                        select new GetProducts
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            Price = p.Price,
                            AvailableQty = p.AvailableQty,
                        };

           var result= await query.SingleOrDefaultAsync();
            return result;
        }

        public async Task<int> UpdateProducts(ProductsInsert products)
        {

            int result = 0;

            var queryget=from p in _myContext.Products where p.Id == products.Id select p;
            var prod = await queryget.FirstOrDefaultAsync();


           // var prod = await _myContext.Products.FindAsync(products.Id);

            if(prod==null)
            {
                return -1;   
            }
            prod.Price= products.Price; 
            prod.ModifiedBy=products.CreatedBy;
            prod.ProductName=products.ProductName;
            prod.AvailableQty=products.AvailableQty;         
            prod.ModifiedBy=products.CreatedBy;         
            prod.ModifiedDate = DateTime.Now;
            prod.IsDeleted = false;
            var update = _myContext.Products.Update(prod);

             result = await _myContext.SaveChangesAsync();
            
            return result; 
        }
    }
}
