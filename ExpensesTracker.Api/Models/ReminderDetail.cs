using System;
using System.Collections.Generic;

namespace ExpenseTracker.Model;

public partial class ReminderDetail
{
    public int ReminderId { get; set; }

    public string RemType { get; set; } = null!;

    public decimal RemAmount { get; set; }

    public DateOnly DueDate { get; set; }

    public string? RemDescription { get; set; }

    public string Email { get; set; } = null!;

    public virtual LoginTb EmailNavigation { get; set; } = null!;
}
