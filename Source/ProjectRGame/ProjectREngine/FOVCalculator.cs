using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ProjectREngine
{
    public static class FOVCalculator
    {
        public static void calculateFOV(Level level)
        {
            level.setLit(level.getHeroLocation(), 0);

            scanOctant1(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant2(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant3(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant4(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant5(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant6(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant7(level, level.getHeroLocation(), 1, 1, 0);
            scanOctant8(level, level.getHeroLocation(), 1, 1, 0);
        }

        private static void scanOctant1(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int y = heroLocation.y - depth;
            int x = heroLocation.x - (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, false) >= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x - 1, y)))
                    {
                        double newEndSlope = getSlope(x - 0.5, y + 0.5, heroLocation.x, heroLocation.y, false);
                        scanOctant1(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x - 1, y)))
                    {
                        startSlope = getSlope(x - 0.5, y - 0.5, heroLocation.x, heroLocation.y, false);
                    }
                }
                level.setLit(new Location(x, y), 0);
                x++;
            }
            x--;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant1(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant2(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int y = heroLocation.y - depth;
            int x = heroLocation.x + (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, false) <= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x + 1, y)))
                    {
                        double newEndSlope = getSlope(x + 0.5, y + 0.5, heroLocation.x, heroLocation.y, false);
                        scanOctant2(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x + 1, y)))
                    {
                        startSlope = -getSlope(x + 0.5, y - 0.5, heroLocation.x, heroLocation.y, false);
                    }
                }
                level.setLit(new Location(x, y), 0);
                x--;
            }
            x++;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant2(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant3(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int x = heroLocation.x + depth;
            int y = heroLocation.y - (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, true) <= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x, y - 1)))
                    {
                        double newEndSlope = getSlope(x - 0.5, y - 0.5, heroLocation.x, heroLocation.y, true);
                        scanOctant3(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x, y - 1)))
                    {
                        startSlope = -getSlope(x + 0.5, y - 0.5, heroLocation.x, heroLocation.y, true);
                    }
                }
                level.setLit(new Location(x, y), 0);
                y++;
            }
            y--;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant3(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant4(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int x = heroLocation.x + depth;
            int y = heroLocation.y + (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, true) >= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x, y + 1)))
                    {
                        double newEndSlope = getSlope(x - 0.5, y + 0.5, heroLocation.x, heroLocation.y, true);
                        scanOctant4(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x, y + 1)))
                    {
                        startSlope = getSlope(x + 0.5, y + 0.5, heroLocation.x, heroLocation.y, true);
                    }
                }
                level.setLit(new Location(x, y), 0);
                y--;
            }
            y++;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant4(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }
        
        private static void scanOctant5(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int y = heroLocation.y + depth;
            int x = heroLocation.x + (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, false) >= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x + 1, y)))
                    {
                        double newEndSlope = getSlope(x + 0.5, y - 0.5, heroLocation.x, heroLocation.y, false);
                        scanOctant5(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x + 1, y)))
                    {
                        startSlope = getSlope(x + 0.5, y + 0.5, heroLocation.x, heroLocation.y, false);
                    }
                }
                level.setLit(new Location(x, y), 0);
                x--;
            }
            x++;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant5(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant6(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int y = heroLocation.y + depth;
            int x = heroLocation.x - (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, false) <= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x - 1, y)))
                    {
                        double newEndSlope = getSlope(x - 0.5, y - 0.5, heroLocation.x, heroLocation.y, false);
                        scanOctant6(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x - 1, y)))
                    {
                        startSlope = -getSlope(x - 0.5, y + 0.5, heroLocation.x, heroLocation.y, false);
                    }
                }
                level.setLit(new Location(x, y), 0);
                x++;
            }
            x--;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant6(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant7(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int x = heroLocation.x - depth;
            int y = heroLocation.y + (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, true) <= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x, y + 1)))
                    {
                        double newEndSlope = getSlope(x + 0.5, y + 0.5, heroLocation.x, heroLocation.y, true);
                        scanOctant7(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x, y + 1)))
                    {
                        startSlope = -getSlope(x - 0.5, y + 0.5, heroLocation.x, heroLocation.y, true);
                    }
                }
                level.setLit(new Location(x, y), 0);
                y--;
            }
            y++;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant7(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static void scanOctant8(Level level, Location heroLocation, int depth, double startSlope, double endSlope)
        {
            if (depth >= Hero.MAX_SIGHT_DISTANCE)
            {
                return;
            }

            int x = heroLocation.x - depth;
            int y = heroLocation.y - (int)(startSlope * depth);

            while (getSlope(x, y, heroLocation.x, heroLocation.y, true) >= endSlope)
            {
                if (level.blocksSight(new Location(x, y)))
                {
                    if (!level.blocksSight(new Location(x, y - 1)))
                    {
                        double newEndSlope = getSlope(x + 0.5, y - 0.5, heroLocation.x, heroLocation.y, true);
                        scanOctant8(level, heroLocation, depth + 1, startSlope, newEndSlope);
                    }
                }
                else
                {
                    if (level.blocksSight(new Location(x, y - 1)))
                    {
                        startSlope = getSlope(x - 0.5, y - 0.5, heroLocation.x, heroLocation.y, true);
                    }
                }
                level.setLit(new Location(x, y), 0);
                y++;
            }
            y--;
            if (depth < Hero.MAX_SIGHT_DISTANCE && !level.blocksSight(new Location(x, y)))
            {
                scanOctant8(level, heroLocation, depth + 1, startSlope, endSlope);
            }
        }

        private static double getSlope(double ax, double ay, double bx, double by, bool invert)
        {
            double dy = ay - by;
            double dx = ax - bx;

            if (invert)
                return dy/dx;
            return dx/dy;
        }
    }
}
