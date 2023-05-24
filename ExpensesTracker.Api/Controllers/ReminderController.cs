using ExpensesTracker.Api.DTOModels;
using ExpenseTracker.Data;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ActionResult> GetAllReminder()
        {
            var reminders = await context.ReminderDetails.ToListAsync();
            if (reminders.Count == 0)
            {
                return BadRequest();
            }
            return Ok(reminders);
        }

        //Get all the reminders details
        [HttpGet("{email}")]
        public async Task<ActionResult> GetReminderByEmail(string email)
        {
            if(email == null)
            {
                return BadRequest();
            }
            else
            {
                var reminders = await context.ReminderDetails.Where(e => e.Email == email).ToListAsync();   
                if(reminders.Count == 0)
                {
                    return NotFound();
                }
                else
                    return Ok(reminders);
            }
        }

        //Post a reminder
        [HttpPost]
        public async Task<ActionResult> PostReminders(ReminderDto postReminder)
        {
            if (postReminder == null)
            {
                return BadRequest();
            }
            else
            {
                var reminder = new ReminderDetail()
                {
                    RemType = postReminder.RemType,
                    RemAmount = postReminder.RemAmount,
                    DueDate = DateOnly.Parse(postReminder.DueDate),
                    RemDescription = postReminder.RemDescription,
                    Email = postReminder.Email
                };
                await context.ReminderDetails.AddAsync(reminder);
                await context.SaveChangesAsync();
                return Ok(postReminder);
            }
        }

        //Update reminder by id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReminderAll(int id, ReminderDto updateReminder)
        {
            var reminder = await context.ReminderDetails.FindAsync(id);
            if(reminder == null)
            {
                return NotFound(id);
            }
            else
            {
                reminder.RemType = updateReminder.RemType;
                reminder.RemAmount = updateReminder.RemAmount;
                reminder.DueDate = DateOnly.Parse(updateReminder.DueDate);
                reminder.RemDescription = updateReminder.RemDescription;
                await context.SaveChangesAsync();
                return Ok(reminder);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateReminderParts(int id,[FromBody] JsonPatchDocument<ReminderDetail>  patchReminder)
        {
            var reminder = await context.ReminderDetails.FindAsync(id);
            if (reminder == null)
            {
                return NotFound(id);
            }
            patchReminder.ApplyTo(reminder, ModelState);
           
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            await context.SaveChangesAsync();
            return Ok(reminder);
        }
        //Get due reminders details
        [HttpGet("DueCheck")]
        public async Task<ActionResult> GetDueReminder()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var reminders = await context.ReminderDetails.Where(e => e.DueDate >= today).ToListAsync();
            if (reminders.Count == 0)
                return NotFound();
            else
                return Ok(reminders);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveReminders(int id)
        {
            var reminder = await context.ReminderDetails.FindAsync(id);
            if(reminder == null)
                return NotFound();
            else
            {
                context.ReminderDetails.Remove(reminder);
                await context.SaveChangesAsync();
                return Ok(reminder);
            }
        }
    }
}
