using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BattleMapServer.Models;

[Index("CharacterName", Name = "UQ__Characte__51594B297EACD2AD", IsUnique = true)]
public partial class Character
{
    [Key]
    public int CharacterId { get; set; }

    public int? UserId { get; set; }

    [StringLength(50)]
    public string CharacterName { get; set; } = null!;

    [StringLength(100)]
    public string? CharacterPic { get; set; }

    [Column("AC")]
    public int Ac { get; set; }

    [Column("HP")]
    public int Hp { get; set; }

    [Column("str")]
    public int Str { get; set; }

    [Column("dex")]
    public int Dex { get; set; }

    [Column("con")]
    public int Con { get; set; }

    [Column("int")]
    public int Int { get; set; }

    [Column("wis")]
    public int Wis { get; set; }

    [Column("cha")]
    public int Cha { get; set; }

    [Column("level")]
    public int Level { get; set; }

    [Column("passive_desc")]
    [StringLength(1000)]
    public string? PassiveDesc { get; set; }

    [Column("action_desc")]
    [StringLength(1000)]
    public string? ActionDesc { get; set; }

    [Column("special_action_desc")]
    [StringLength(1000)]
    public string? SpecialActionDesc { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Characters")]
    public virtual User? User { get; set; }
}
