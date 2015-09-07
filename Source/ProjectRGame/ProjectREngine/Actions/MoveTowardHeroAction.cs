using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class MoveTowardHeroAction : Action
    {
        public override bool doAction()
        {
            Location target = AStarSearch.getFirstStep(actor.location,
                                                       level.getHeroLocation(),
                                                       level);

            Direction directionToTarget = Direction.None;
            if (actor.location.x < target.x)
            {
                directionToTarget = Direction.East;
            }
            else if (actor.location.x > target.x)
            {
                directionToTarget = Direction.West;
            }
            else if (actor.location.y < target.y)
            {
                directionToTarget = Direction.South;
            }
            else if (actor.location.y > target.y)
            {
                directionToTarget = Direction.North;
            }

            alternate = new MoveAction(directionToTarget);
            return false;
        }
    }
}
