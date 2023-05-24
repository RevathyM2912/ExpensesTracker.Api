using ExpensesTracker.Api.DTOModels;
using ExpenseTracker.Data;
using ExpenseTracker.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseTrackerContext _context;

        public ExpenseController(ExpenseTrackerContext dbcontext)
        {
            _context = dbcontext;
        }
        //Get all expenses
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _context.ExpenseDetails.ToListAsync());
        }
        [HttpGet("{email}")]
        public async Task<ActionResult> GetExpenseByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Please enter an email address");
            }
            else
            {
                var expenses = await _context.ExpenseDetails.Where(e => e.ProEmail == email).ToListAsync();
                if (expenses.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(expenses);
                }
            }
        }
        [HttpGet("Date")]
        public async Task<ActionResult> GetExpensesByDate(DateTime date, string email)
        {
            if (email == null)
            {
                return BadRequest();
            }
            else
            {
                DateOnly expenseDate = DateOnly.FromDateTime(date);
                var expeses = await _context.ExpenseDetails.Where(e => e.ExpenseDate == expenseDate && e.ProEmail == email).ToListAsync();
                if (expeses.Count == 0)
                    return NotFound();
                else
                    return Ok(expeses);
            }
        }

        //Post an expense
        [HttpPost]
        public async Task<ActionResult> PostAnExpense(ExpenseDetails postExpense)
        {
            if (postExpense == null) 
                return BadRequest();
            else
            {
                var expense = new ExpenseDetail()
                {
                    ExpenseCategory = postExpense.ExpenseCategory,
                    ExpenseAmount = postExpense.ExpenseAmount,
                    ExpenseDate = DateOnly.Parse(postExpense.ExpenseDate),
                    ProEmail = postExpense.ProEmail,
                    ExpenseDescription = postExpense.ExpenseDescription
                };
                await _context.ExpenseDetails.AddAsync(expense);
                await _context.SaveChangesAsync();
                return Ok(expense);
            }
        }

        //Update expense record
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateExpense(int id, ExpenseDetails putExpense)
        {
            if (id == 0)
                return BadRequest();
            else
            {
                var expense = await _context.ExpenseDetails.FindAsync(id);
                if (expense == null)
                    return NotFound();
                else
                {
                    expense.ExpenseCategory = putExpense.ExpenseCategory;
                    expense.ExpenseAmount = putExpense.ExpenseAmount;
                    expense.ExpenseDate = DateOnly.Parse(putExpense.ExpenseDate);
                    expense.ExpenseDescription = putExpense.ExpenseDescription;
                    await _context.SaveChangesAsync();
                    return Ok(expense);
                }
            }
        }

        //Delete expense details
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteExpenseById(int id)
        {
            var expense = await _context.ExpenseDetails.FindAsync(id);
            if(expense == null)
                return NotFound();
            else
            {
                _context.ExpenseDetails.Remove(expense);
                await _context.SaveChangesAsync();
                return Ok($"Record removed\n{expense}");
            } 
        }
    }
}
