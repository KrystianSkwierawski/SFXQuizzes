using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Quiz : AuditableEntity
{
    public string Id { get; set; }

    public string Title { get; set; }

    public bool IsPublic { get; set; }

    public bool Approved { get; set; } = false;

    [NotMapped]
    public IList<string> SFXNames { get; set; }
}

