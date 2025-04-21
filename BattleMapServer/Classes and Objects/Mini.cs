using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Platform;
using BattleMapServer.DTO;

namespace BattleMapServer.Classes_and_Objects
{
    public class Mini
    {

        public Monster monster;
        public Character character;
        public int currentHP;
        public Cords location;
        public static List<Mini> AllMinis = new List<Mini>();
        public Microsoft.Maui.Graphics.IImage img;        

        public string Name { get; set; }

        public string ImgURL {  get; set; }                           

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
        
        public Mini() { }
    }
}
