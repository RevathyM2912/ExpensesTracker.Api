using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Model;

public partial class ExpenseDetail
{
    [Key]
    public int ExpenseId { get; set; }

    public string ExpenseCategory { get; set; } = null!;

    public decimal ExpenseAmount { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string? ExpenseDescription { get; set; }

    public string ProEmail { get; set; } = null!;

    public virtual LoginTb ProEmailNavigation { get; set; } = null!;
}
