using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BattleMapServer.Models;

[Index("UserEmail", Name = "UQ__Users__08638DF8387132CE", IsUnique = true)]
[Index("UserName", Name = "UQ__Users__C9F28456E8AB8086", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string UserEmail { get; set; } = null!;

    [StringLength(50)]
    public string UserPassword { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    [InverseProperty("User")]
    public virtual ICollection<Monster> Monsters { get; set; } = new List<Monster>();
}
