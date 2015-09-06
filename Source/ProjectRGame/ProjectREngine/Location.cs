using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public struct Location
    {
        public int x;
        public int y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Location getAdjLocation(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Location(x, y - 1);
                case Direction.South:
                    return new Location(x, y + 1);
                case Direction.West:
                    return new Location(x - 1, y);
                case Direction.East:
                    return new Location(x + 1, y);
                case Direction.NorthWest:
                    return new Location(x - 1, y - 1);
                case Direction.SouthWest:
                    return new Location(x - 1, y + 1);
                case Direction.NorthEast:
                    return new Location(x + 1, y - 1);
                case Direction.SouthEast:
                    return new Location(x + 1, y + 1);
                default:
                    return this;
            }
        }

        public double distance(Location other)
        {
            int dx = other.x - x;
            int dy = other.y - y;

            return Math.Sqrt(dx*dx + dy*dy);
        }
    }

    public enum Direction
    {
        None, North, South, West, East, NorthWest, SouthWest, NorthEast, SouthEast
    }
}
