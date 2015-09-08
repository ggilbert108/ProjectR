using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Monsters
{
    public class Bat : Monster
    {
        public Bat() : base(DrawTag.Bat, "Bat")
        {
            str = 15;
            def = 5;
            dex = 15;
            speed = 100;
            hp = 80;
            maxHp = hp;
        }
    }
}
