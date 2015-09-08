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

            Actor actorAtLocation = level.getActor(newLocation);
            if (actorAtLocation != null)
            {
                alternate = new MoveTowardActorAction(actorAtLocation);
                return false;
            }


            Entity walkableAtLocation = level.getWalkable(newLocation);
            if (walkableAtLocation is Door)
            {
                alternate = new OpenDoorAction((Door)walkableAtLocation);
                return false;
            }
            if (walkableAtLocation is Chest)
            {
                alternate = new OpenChestAction((Chest)walkableAtLocation);
                return false;
            }
            if (walkableAtLocation is Staircase)
            {
                alternate = new ChangeLevelAction(((Staircase) walkableAtLocation).goesUp);
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


            actor.location = newLocation;

            return true;
        }

        private Location getLocationInDirection(Direction direction)
        {
            Location location = actor.location;

            switch (direction)
            {
                case Direction.North:
                    location.y--;
                    break;
                case Direction.South:
                    location.y++;
                    break;
                case Direction.West:
                    location.x--;
                    break;
                case Direction.East:
                    location.x++;
                    break;
            }

            return location;
        }
    }
}
