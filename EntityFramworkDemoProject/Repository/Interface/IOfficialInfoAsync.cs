using EntityFramworkDemoProject.Models;

namespace EntityFramworkDemoProject.Repository.Interface
{
    public interface IOfficialInfoAsync
    {
        Task<Officialinfo> GetProfiteLossByDate(DateTime fromDate, DateTime toDate);

    }
}
