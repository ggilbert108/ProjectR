﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
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

            Door doorAtLocation = level.getDoor(newLocation);
            if (actor.canOpenDoors && doorAtLocation != null && doorAtLocation.closed)
            {
                doorAtLocation.closed = false;
                return false;
            }

            if (actor is Hero)
            {
                Item itemAtLocation = level.getItem(newLocation);
                level.removeItem(newLocation);
                ((Hero) actor).giveItem(itemAtLocation);
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
