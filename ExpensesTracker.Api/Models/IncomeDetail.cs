using System;
using System.Collections.Generic;

namespace ExpenseTracker.Model;

public partial class IncomeDetail
{
    public int IncomeId { get; set; }

    public string Category { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }

    public string Email { get; set; } = null!;
}
