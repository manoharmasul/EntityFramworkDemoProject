using EntityFramworkDemoProject.Models;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Repository.Interface
{
    public interface IUserRepository
    {
        Task<int> RegisterNewUser(UserInsertModel user);
        Task<int> UpdateUser(UserUpdatetModel user);
        Task<List<GetUserModel>> GetAllUsers();
       // Task<List<tblUser>> GetAllUsersLinq();
        Task<GetUserModel> GetUserById(long id);
        Task<int> DeleteUser(DeleteObj deleteobj);

    }
}
