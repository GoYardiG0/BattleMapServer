using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMapServer.Classes_and_Objects
{
    public class MapDetails
    {
        public List<List<Mini>> AllMinis
        {
            get; set;
        }
        public List<Line> Lines
        {
            get; set;
        }
        public MapDetails() { Lines = new List<Line>(); }
    }
}
