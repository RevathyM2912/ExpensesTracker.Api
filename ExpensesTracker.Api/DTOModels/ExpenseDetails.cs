using Newtonsoft.Json;

namespace ExpensesTracker.Api.DTOModels
{
    public class ExpenseDetails
    {
        public int ExpenseId { get; set; }

        public string ExpenseCategory { get; set; } = null!;

        public decimal ExpenseAmount { get; set; }

        public string ExpenseDate { get; set; } = null!;

        public string? ExpenseDescription { get; set; }

        public string ProEmail { get; set; } = null!;
    }
}
