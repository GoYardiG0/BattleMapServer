﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMapServer.Classes_and_Objects
{
    public class Cords
    {
        public int row {  get; set; }
        public int col {  get; set; }
        public Cords(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public Cords() { }

    }
}
