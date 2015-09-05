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
                case Direction.Up:
                    return new Location(x, y - 1);
                case Direction.Down:
                    return new Location(x, y + 1);
                case Direction.Left:
                    return new Location(x - 1, y);
                case Direction.Right:
                    return new Location(x + 1, y);
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
        None, Up, Down, Left, Right
    }
}
