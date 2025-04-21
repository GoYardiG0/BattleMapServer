using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace BattleMapServer.Classes_and_Objects
{
    public class Line
    {
        public Point start;
        public Point end;
        public Line(Point start, Point end)
        {
            this.start = new Point(start.X,start.Y);
            this.end = new Point(end.X,end.Y);
        }
        public Line() { }
    }
}
