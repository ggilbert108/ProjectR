using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class MoveRandomlyAction : Action
    {
        public override bool doAction()
        {
            Direction[] directions = {Direction.North, Direction.East, Direction.South, Direction.West};

            Util.shuffle(directions);
            
            Location newLocation = new Location(-1, -1);

            Direction direction = Direction.None;
            for(int i = 0; i < 4; i++)
            {
                Location adjLocation = actor.location.getAdjLocation(directions[i]);
                Tile tile = level.getTile(adjLocation);
                if (tile != null && !level.getTile(adjLocation).blocksMovement)
                {
                    direction = directions[i];
                    break;
                }
            }

            if (actor is Monster)
            {
                checkCanSeePlayer();
            }


            alternate = new MoveAction(direction);
            return false;
        }

        private void checkCanSeePlayer()
        {
            if (((Monster) actor).canSeePlayer)
                return;

            List<Location> lerp = Lerp.lerp(actor.location, level.getHeroLocation());
            foreach (Location location in lerp)
            {
                if (level.blocksSight(location))
                    return;
            }

            MessageLog.log("A " + actor.name + " sighted the hero");

            ((Monster) actor).canSeePlayer = true;
        }
    }
}
