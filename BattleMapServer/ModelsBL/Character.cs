namespace BattleMapServer.Models
{
    public partial class Character
    {
        public void ReSetCharacter(DTO.Character dtoCharacter)
        {
            this.CharacterId = dtoCharacter.CharacterId;
            this.UserId = dtoCharacter.UserId;
            this.CharacterName = dtoCharacter.CharacterName;
            this.CharacterPic = dtoCharacter.CharacterPic;
            this.Ac = dtoCharacter.Ac;
            this.Hp = dtoCharacter.Hp;
            this.Str = dtoCharacter.Str;
            this.Dex = dtoCharacter.Dex;
            this.Con = dtoCharacter.Con;
            this.Int = dtoCharacter.Int;
            this.Wis = dtoCharacter.Wis;
            this.Cha = dtoCharacter.Cha;
            this.Level = dtoCharacter.Level;
            this.PassiveDesc = dtoCharacter.PassiveDesc;
            this.ActionDesc = dtoCharacter.ActionDesc;
            this.SpecialActionDesc = dtoCharacter.SpecialActionDesc;

        }
    }
}
