using BattleMapServer.Models;

namespace BattleMapServer.DTO
{
    public class Monster
    {
        public int MonsterId { get; set; }

        public int UserId { get; set; }

        public string MonsterName { get; set; } = null!;

        public string? MonsterPic { get; set; }

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

        public Monster() { }
        public Monster(Models.Monster modelMonster)
        {
            this.MonsterId = modelMonster.MonsterId;
            this.UserId = modelMonster.UserId;
            this.MonsterName = modelMonster.MonsterName;
            this.MonsterPic = modelMonster.MonsterPic;
            this.Ac = modelMonster.Ac;
            this.Hp = modelMonster.Hp;
            this.Str = modelMonster.Str;
            this.Dex = modelMonster.Dex;
            this.Con = modelMonster.Con;
            this.Int = modelMonster.Int;
            this.Wis = modelMonster.Wis;
            this.Cha = modelMonster.Cha;
            this.Cr = modelMonster.Cr;
            this.PassiveDesc = modelMonster.PassiveDesc;
            this.ActionDesc = modelMonster.ActionDesc;
            this.SpecialActionDesc = modelMonster.SpecialActionDesc;

        }

        public Models.Monster GetModels()
        {
            Models.Monster modelsMonster = new Models.Monster()
            {
                MonsterId = this.MonsterId,
                UserId = this.UserId,
                MonsterName = this.MonsterName,
                MonsterPic = this.MonsterPic,
                Ac = this.Ac,
                Hp = this.Hp,
                Str = this.Str,
                Dex = this.Dex,
                Con = this.Con,
                Int = this.Int,
                Wis = this.Wis,
                Cha = this.Cha,
                Cr = this.Cr,
                PassiveDesc = this.PassiveDesc,
                ActionDesc = this.ActionDesc,
                SpecialActionDesc = this.SpecialActionDesc,
            };

            return modelsMonster;
        }

    }
}
