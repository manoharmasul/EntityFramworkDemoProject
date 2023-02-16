using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramworkDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficialInfoController : ControllerBase
    {
        private readonly IOfficialInfoAsync _officialInfo;
        private readonly ILogger<OrderController> logger;
        IConfiguration configuration;
        public OfficialInfoController(IOfficialInfoAsync officialInfoAsync, ILogger<OrderController> logger, IConfiguration configuration)
        {
            _officialInfo = officialInfoAsync;
            this.logger = logger;
            this.configuration = configuration;
        }
        [HttpGet("GetProfiteLossByDate")]
        public async Task<IActionResult> GetProfiteLossByDate(DateTime fromDate,DateTime toDate)
        {
            var result=await _officialInfo.GetProfiteLossByDate(fromDate, toDate);

            return Ok(result);

        }
    }
}
