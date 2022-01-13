using Domain.Common;

namespace Domain.Entities;

public class Quiz : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool Public { get; set; } = false;

    public bool Approved { get; set; } = false;
}

