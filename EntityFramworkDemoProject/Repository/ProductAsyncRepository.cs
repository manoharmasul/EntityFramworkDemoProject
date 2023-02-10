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

        public async Task<int> AddNewProducts(Products products)
        {  
                
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

        public async Task<List<Products>> GetAllProducts()
        {
            var result = await _myContext.Products.ToListAsync();
            return result;
        }

        public async Task<Products> GetProductById(long id)
        {
            var result = await _myContext.Products.FindAsync(id);

            return result;
        }

        public async Task<int> UpdateProducts(UpdateProduct products)
        {
            int result = 0;
            var prod=await _myContext.Products.FindAsync(products.Id);
            if(prod==null)
            {
                return -1;   
            }
            prod.Price= products.Price; 
            prod.ModifiedBy=products.ModifiedBy;
            prod.ProductName=products.ProductName;
            prod.AvailableQty=products.AvailableQty;         
            prod.ModifiedDate = DateTime.Now;
            prod.IsDeleted = false;
            var query = _myContext.Products.Update(prod);

             result = await _myContext.SaveChangesAsync();
            
            return result; 
        }
    }
}
