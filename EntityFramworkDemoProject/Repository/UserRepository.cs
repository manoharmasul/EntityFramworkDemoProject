using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Context;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static EntityFramworkDemoProject.Model.BaseModel;
using System;

namespace EntityFramworkDemoProject.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly MyContext _mycontext;   
        public UserRepository(MyContext mycontext)
        {
            _mycontext = mycontext;
        }

        public async Task<int> DeleteUser(DeleteObj deleteObj)
        {
            var result = 0;
            var user = await _mycontext.tblUser.FindAsync(deleteObj.Id);

            if(user !=null)
            {
                user.ModifiedBy = deleteObj.ModifiedBy;
                user.ModifiedDate=DateTime.Now;
                user.IsDeleted = true;

                var query = _mycontext.tblUser.Update(user);

                 result = await _mycontext.SaveChangesAsync();
            }


            return result;
        }

        public async Task<List<GetUserModel>> GetAllUsers()
        {
            //  Id,UserName,EmailId,MobileNo,WalletBalanc

            var query = from u in _mycontext.tblUser where u.IsDeleted == false 

                      select new GetUserModel 
                      { 
                          Id=u.Id,UserName=u.UserName,
                          EmailId=u.EmailId,
                          MobileNo=u.MobileNo,
                          WalletBalance=u.WalletBalance
                      };

           var  ulist = await query.ToListAsync();

            return ulist.ToList();
        }

      

        public async Task<GetUserModel> GetUserById(long id)
        {


            var query = from u in _mycontext.tblUser
                        where u.Id == id && u.IsDeleted == false
                        select new GetUserModel
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            EmailId = u.EmailId,
                            MobileNo = u.MobileNo,
                            WalletBalance = u.WalletBalance
                        };
            var userlist = await query.FirstOrDefaultAsync();


            return userlist;
        }



        public async Task<int> RegisterNewUser(UserInsertModel user)
        {

            var checkqueryEmail = from u in _mycontext.tblUser where u.EmailId==user.EmailId && u.IsDeleted==false
                             select u;

            var usercheckEmail =await checkqueryEmail.SingleOrDefaultAsync<tblUser>();
            if(usercheckEmail!=null)
            {
                return -1;//Email Id Duplicate
            }
            var checkqueryMobile = from u in _mycontext.tblUser.Where(u => u.MobileNo == user.MobileNo && u.IsDeleted == false)select u;
           var  usercheckqueryMobile = await checkqueryMobile.SingleOrDefaultAsync<tblUser>();

            if (usercheckqueryMobile != null)
            {
                return -2;//Mobile No Duplicate
            }
           



            tblUser ur = new tblUser();
            ur.CreatedDate = DateTime.Now;
            ur.ModifiedDate = DateTime.Now;
            ur.ModifiedBy = 0;
            


            ur.UserName = user.UserName;
            ur.EmailId = user.EmailId;
            ur.Password = user.Password;
            ur.WalletBalance = user.WalletBalance;
            ur.MobileNo = user.MobileNo;
            ur.IsDeleted = false;
            ur.IsDeleted = false;
            ur.UserType = 2;
            ur.CreatedBy = user.CreatedBy;

            var query = _mycontext.AddAsync(ur);

            int res = await _mycontext.SaveChangesAsync();
      
            return res;

           
        }

        public async Task<int> UpdateUser(UserUpdatetModel user)
        {

            var result = 0;
            var user1 = await _mycontext.tblUser.FindAsync(user.Id);
            

            if (user1 != null)
            {
                user1.UserName = user.UserName; 
                user1.EmailId= user.EmailId;    
                user1.Password=user.Password;
                user1.WalletBalance=user.WalletBalance;
                user1.ModifiedBy= user.ModifiedBy;
                user1.ModifiedDate= DateTime.Now;
                user1.UserType= 2;



                _mycontext.tblUser.Update(user1);

                 result = await _mycontext.SaveChangesAsync();
            }
            return result;
        }

     
    }
}
