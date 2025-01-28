namespace BattleMapServer.Models
{
    public partial class Monster
    {
        public void ReSetMonster(DTO.Monster dtoMonster)
        {
            this.MonsterId = dtoMonster.MonsterId;
            this.UserId = dtoMonster.UserId;
            this.MonsterName = dtoMonster.MonsterName;
            this.MonsterPic = dtoMonster.MonsterPic;
            this.Ac = dtoMonster.Ac;
            this.Hp = dtoMonster.Hp;
            this.Str = dtoMonster.Str;
            this.Dex = dtoMonster.Dex;
            this.Con = dtoMonster.Con;
            this.Int = dtoMonster.Int;
            this.Wis = dtoMonster.Wis;
            this.Cha = dtoMonster.Cha;
            this.Cr = dtoMonster.Cr;
            this.PassiveDesc = dtoMonster.PassiveDesc;
            this.ActionDesc = dtoMonster.ActionDesc;
            this.SpecialActionDesc = dtoMonster.SpecialActionDesc;

        }
    }
}
