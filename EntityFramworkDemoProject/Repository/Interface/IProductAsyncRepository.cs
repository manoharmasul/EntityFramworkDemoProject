using EntityFramworkDemoProject.Models;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Repository.Interface
{
    public interface IProductAsyncRepository
    {
        Task<int> AddNewProducts(Products products);
        Task<int> UpdateProducts(UpdateProduct products);
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProductById(long id);
        Task<int> DeleteProducts(DeleteObj deleteobj);
    }
}
