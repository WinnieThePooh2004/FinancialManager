using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Controllers
{
    [ApiController]
    [Route("/")]
    public class Home : Controller
    {
        [HttpGet]
        public async Task<ActionResult<int>> Index()
        {
            await Task.Delay(1);
            return 1;
        }
    }
}
