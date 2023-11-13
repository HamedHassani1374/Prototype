using System;
using System.Collections.Generic;

namespace Prototype.Model.Models;

public partial class User : IBaseEntity
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public byte[]? Password { get; set; }

    public string? UserFullName { get; set; }

    public int? UserRollId { get; set; }
}
