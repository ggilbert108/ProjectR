using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Hero : Actor
    {
        public Hero() : base(DrawTag.Hero, "hero")
        {
            drawPriority = 0;
        }

        public override Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                result = ActionResult.PlayerWait;
                return null;
            }
            else
            {
                return base.getNextAction(ref result);
            }
        }
    }
}
