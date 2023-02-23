using EntityFramworkDemoProject.Context;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Security.Cryptography;

namespace EntityFramworkDemoProject.Repository
{
    public class OfficialInfoAsync : IOfficialInfoAsync
    {
      
        private readonly MyContext _myContext;

        public OfficialInfoAsync(MyContext context)
        {
            _myContext = context;
        }
        public async Task<Officialinfo> GetProfiteLossByDate(DateTime fromDate, DateTime toDate)
        {
            Officialinfo official=new Officialinfo();



            var query = from o in _myContext.Order
                        where o.IsDeleted==false && (o.OrderDate).Date >= fromDate.Date  && (o.OrderDate).Date<= toDate.Date
                        select new Order
                        {
                           
                            TotalAmmount = o.TotalAmmount,
                        };

            official.TotalSell = await query.Select(t=>t.TotalAmmount).SumAsync();
            official.TotalProfit = (official.TotalSell / 100) * 15;

            official.TimePeriod = (toDate - fromDate).Days+" Days";

            return official;

        }
    }
}
