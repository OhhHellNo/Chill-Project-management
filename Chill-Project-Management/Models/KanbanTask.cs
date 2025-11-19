using System;
using System.Collections.Generic;

namespace Chill_Project_Management.Models;

public partial class KanbanTask
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string State { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public DateOnly? DueDate { get; set; }

    public int? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }
}
