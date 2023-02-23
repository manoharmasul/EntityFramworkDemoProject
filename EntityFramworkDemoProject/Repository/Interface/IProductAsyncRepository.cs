using EntityFramworkDemoProject.Models;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Repository.Interface
{
    public interface IProductAsyncRepository
    {
        Task<int> AddNewProducts(ProductsInsert prod);
        Task<int> UpdateProducts(ProductsInsert products);
        Task<List<GetProducts>> GetAllProducts();
        Task<GetProducts> GetProductById(long id);
        Task<int> DeleteProducts(DeleteObj deleteobj);
    }
}
