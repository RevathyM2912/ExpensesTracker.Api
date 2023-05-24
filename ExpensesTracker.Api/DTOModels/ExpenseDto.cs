using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Api.DTOModels
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public string ExpenseCategory { get; set; } = null!;
       
        [Required(ErrorMessage = "Amount is required")]
        public decimal ExpenseAmount { get; set; }
        
        [Required(ErrorMessage = "Date is required")]
        public string ExpenseDate { get; set; } = null!;

        public string? ExpenseDescription { get; set; }
       
        [EmailAddress(ErrorMessage ="Please entre a valid email address")]
        public string ProEmail { get; set; } = null!;
    }
}
