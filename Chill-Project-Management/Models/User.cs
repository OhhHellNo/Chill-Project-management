using System;
using System.Collections.Generic;

namespace Chill_Project_Management.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Username { get; set; }

    public virtual ICollection<KanbanTask> KanbanTasks { get; set; } = new List<KanbanTask>();
}
