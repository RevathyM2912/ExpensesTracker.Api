using ExpenseTracker.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly ExpenseTrackerContext context;

        public ReminderController(ExpenseTrackerContext dbContext)
        {
            context = dbContext;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetAllReminder()
        //{

        //}
    }
}
