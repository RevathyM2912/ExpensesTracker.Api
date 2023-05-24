using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Model;

public partial class LoginTb
{
    public string ProName { get; set; } = null!;
    public string ProEmail { get; set; } = null!;
    public string ProPassword { get; set; }
    public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; } = new List<ExpenseDetail>();
    public virtual ICollection<ReminderDetail> ReminderDetails { get; set; } = new List<ReminderDetail>();
}
