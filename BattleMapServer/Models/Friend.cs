using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BattleMapServer.Models;

[Keyless]
public partial class Friend
{
    public int? UserId { get; set; }

    public int? FriendId { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
