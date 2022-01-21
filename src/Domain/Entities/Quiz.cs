using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Quiz : AuditableEntity
{
    public string Id { get; set; }

    public string Title { get; set; }

    public bool IsPublic { get; set; }

    public bool Approved { get; set; } = false;

    public IList<SFXName> SFXNames { get; set; }
}

