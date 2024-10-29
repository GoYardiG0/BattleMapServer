﻿using BattleMapServer.Models;

namespace BattleMapServer.DTO
{
    public class Monster
    {
        public int MonsterId { get; set; }

        public int? UserId { get; set; }

        public string MonsterName { get; set; } = null!;

        public int Ac { get; set; }

        public int Hp { get; set; }

        public int Str { get; set; }

        public int Dex { get; set; }

        public int Con { get; set; }

        public int Int { get; set; }

        public int Wis { get; set; }

        public int Cha { get; set; }

        public int Cr { get; set; }

        public string? PassiveDesc { get; set; }

        public string? ActionDesc { get; set; }

        public string? SpecialActionDesc { get; set; }

    }
}
