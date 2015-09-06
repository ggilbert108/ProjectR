using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectREngine.Actions;

namespace ProjectREngine
{
    public abstract class Monster : Actor
    {
        protected Monster(DrawTag drawTag, string name) : base(drawTag, name)
        {
            str = 10;
            def = 10;
            dex = 10;
            speed = 80;
            hp = 100;
            maxHp = hp;

            faction = Faction.Evil;
        }

        public override Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                setNextAction(new MoveRandomlyAction());
            }
            return base.getNextAction(ref result);
        }
    }
}
