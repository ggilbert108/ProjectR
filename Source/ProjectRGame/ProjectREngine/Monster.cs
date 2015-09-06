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
