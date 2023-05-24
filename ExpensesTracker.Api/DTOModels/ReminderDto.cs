using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Api.DTOModels
{
    public class ReminderDto
    {
        [Required(ErrorMessage = "Category is required")]
        public string RemType { get; set; } = null!;

        [Required(ErrorMessage = "Amount is required")]
        public decimal RemAmount { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public string DueDate { get; set; }
        public string? RemDescription { get; set; }

        [EmailAddress(ErrorMessage = "Please entre a valid email address")]
        public string Email { get; set; } = null!;

    }
}
