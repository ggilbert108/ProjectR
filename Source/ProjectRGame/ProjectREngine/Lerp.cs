using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectREngine
{
    public static class Lerp
    {
        public static List<Location> lerp(Location from, Location to)
        {
            double deltaX = to.x - from.x;
            double deltaY = from.y - to.y;
            double error = 0.0;
            double deltaerr = Math.Abs(deltaY/deltaX);

            if (Math.Abs(deltaX) < 0.1)
                return lerpVert(from, to);

            List<Location> lerp = new List<Location>();

            int y = from.y;

            int sx = (from.x < to.x) ? 1 : -1;
            int sy = (from.y < to.y) ? 1 : -1;

            for (int x = from.x; x != to.x; x += sx)
            {
                lerp.Add(new Location(x, y));
                error += deltaerr;
                while (error >= 0.5)
                {
                    y = y + sy;
                    error--;
                    lerp.Add(new Location(x, y));
                }
            }

            return lerp;
        }

        private static List<Location> lerpVert(Location from, Location to)
        {
            List<Location> lerp = new List<Location>();
            int dy = (from.y < to.y) ? 1 : -1;

            for (int y = from.y; y != to.y; y += dy)
            {
                lerp.Add(new Location(from.x, y));
            }

            return lerp;
        }
    }
}
