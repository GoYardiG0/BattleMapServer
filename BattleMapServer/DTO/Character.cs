﻿using BattleMapServer.Models;

namespace BattleMapServer.DTO
{
    public class Character
    {
        public int CharacterId { get; set; }

        public int? UserId { get; set; }

        public string CharacterName { get; set; } = null!;

        public string? CharacterPic { get; set; }

        public int Ac { get; set; }

        public int Hp { get; set; }

        public int Str { get; set; }

        public int Dex { get; set; }

        public int Con { get; set; }

        public int Int { get; set; }

        public int Wis { get; set; }

        public int Cha { get; set; }

        public int Level { get; set; }

        public string? PassiveDesc { get; set; }

        public string? ActionDesc { get; set; }

        public string? SpecialActionDesc { get; set; }

        public Character()
        {

        }
        public Character(Models.Character modelCharacter)
        {
            this.CharacterId = modelCharacter.CharacterId;
            this.UserId = modelCharacter.UserId;
            this.CharacterName = modelCharacter.CharacterName;
            this.CharacterPic = modelCharacter.CharacterPic;
            this.Ac = modelCharacter.Ac;
            this.Hp = modelCharacter.Hp;
            this.Str = modelCharacter.Str;
            this.Dex = modelCharacter.Dex;
            this.Con = modelCharacter.Con;
            this.Int = modelCharacter.Int;
            this.Wis = modelCharacter.Wis;
            this.Cha = modelCharacter.Cha;
            this.Level = modelCharacter.Level;
            this.PassiveDesc = modelCharacter.PassiveDesc;
            this.ActionDesc = modelCharacter.ActionDesc;
            this.SpecialActionDesc = modelCharacter.SpecialActionDesc;

        }

        public Models.Character GetModels()
        {
            Models.Character modelsCharacter = new Models.Character()
            {
                CharacterId = this.CharacterId,
                UserId = this.UserId,
                CharacterName = this.CharacterName,
                CharacterPic = this.CharacterPic,
                Ac = this.Ac,
                Hp = this.Hp,
                Str = this.Str,
                Dex = this.Dex,
                Con = this.Con,
                Int = this.Int,
                Wis = this.Wis,
                Cha = this.Cha,
                Level = this.Level,
                PassiveDesc = this.PassiveDesc,
                ActionDesc = this.ActionDesc,
                SpecialActionDesc = this.SpecialActionDesc,
            };

            return modelsCharacter;
        }
    }
}
