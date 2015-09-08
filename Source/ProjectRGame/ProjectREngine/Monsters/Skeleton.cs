using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Monsters
{
    public class Skeleton : Monster
    {
        public Skeleton() : base(DrawTag.Skeleton, "Skeleton")
        {
            str = 30;
            def = 10;
            dex = 10;
            speed = 80;
            hp = 100;
            maxHp = hp;
        }
    }
}
