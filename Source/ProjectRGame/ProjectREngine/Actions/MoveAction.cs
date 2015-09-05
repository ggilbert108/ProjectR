using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using ProjectREngine.Actions;

namespace ProjectREngine.Actions
{
    public class MoveAction : Action
    {
        private readonly Direction _direction;

        public MoveAction(Direction direction)
        {
            _direction = direction;
        }

        public override bool doAction()
        {
            Location newLocation = getLocationInDirection(_direction);

            Tile tileAtLocation = level.getTile(newLocation);
            if (tileAtLocation != null && tileAtLocation.blocksMovement)
            {
                return false;
            }

            Entity walkableAtLocation = level.getWalkable(newLocation);
            if (walkableAtLocation is Door)
            {
                alternate = new OpenDoorAction((Door)walkableAtLocation);
                return false;
            }

            if (actor is Hero)
            {
                Item itemAtLocation = level.getItem(newLocation);
                if (itemAtLocation != null)
                {
                    alternate = new PickUpItemAction(itemAtLocation);
                    return false;
                }
            }

            //TODO decouple actions

            Actor actorAtLocation = level.getActor(newLocation);
            if (actorAtLocation != null)
            {
                alternate = new MoveTowardActorAction(actorAtLocation);
                return false;
            }

            actor.location = newLocation;

            return true;
        }

        private Location getLocationInDirection(Direction direction)
        {
            Location location = actor.location;

            switch (direction)
            {
                case Direction.Up:
                    location.y--;
                    break;
                case Direction.Down:
                    location.y++;
                    break;
                case Direction.Left:
                    location.x--;
                    break;
                case Direction.Right:
                    location.x++;
                    break;
            }

            return location;
        }
    }
}
