using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectREngine
{
    public static class DungeonGenerator
    {
        public static void generateDungeon(Level level, int width, int height)
        {
            Rect dungeonRect = new Rect(0, 0, width, height);
            level.bounds = dungeonRect;

            carveRoom(level, dungeonRect, true, TileType.Dungeon_Wall);

            Location center = new Location(width / 2, height / 2);
            Rect innerRoom = createRect(center, Direction.West, Feature.Room);
            carveRoom(level, innerRoom, false, TileType.Dungeon_Floor);

            for (int i = 0; i < 100; i++)
            {
                Location doorLoc = findDoor(level, dungeonRect);

                //randomly choose a feature to add to the dungeon
                Feature[] features = {Feature.Corridor, Feature.Room};
                Feature feature = Util.chooseRandomElement(features);

                tryAddFeature(level, doorLoc, feature);
            }
            addEntranceAndExit(level, dungeonRect);
            addChests(level, dungeonRect);
            initLighting(level, dungeonRect);
        }

        private static void tryAddFeature(Level level, Location doorLoc, Feature feature)
        {
            Direction[] directions = { Direction.North, Direction.South, Direction.West, Direction.East };

            foreach (Direction direction in directions)
            {
                Rect rect = createRect(doorLoc, direction, feature);
                rect.expand();
                if (rectIsClear(level, rect))
                {
                    rect.compress();
                    if (feature == Feature.Room)
                    {
                        carveRoom(level, rect, false, TileType.Dungeon_Floor);
                    }
                    else if (feature == Feature.Corridor)
                    {
                        carveCorridor(level, rect, false, TileType.Dungeon_Floor);
                    }
                    level.addTile(new Tile(false, TileType.Dungeon_Floor), doorLoc);
                    level.addWalkable(new Door(), doorLoc);
                    break;
                }
            }
        }

        private static Location findDoor(Level level, Rect dungeonRect)
        {
            Location doorLoc = new Location(0, 0);

            while (!isDoorCandidate(level, doorLoc))
            {
                doorLoc = new Location(
                    Util.random.Next(dungeonRect.x2),
                    Util.random.Next(dungeonRect.y2));
            }

            return doorLoc;
        }

        private static void carveRoom(Level level, Rect rect, bool blocks, TileType tileType)
        {
            for (int x = rect.x1; x <= rect.x2; x++)
            {
                for (int y = rect.y1; y <= rect.y2; y++)
                {
                    Tile tile = new Tile(blocks, tileType);
                    level.addTile(tile, new Location(x, y));
                }
            }
        }

        private static void carveCorridor(Level level, Rect rect, bool blocks, TileType tileType)
        {
            rect.compressCorridor();
            carveRoom(level, rect, blocks, tileType);
        }

        private static bool rectIsClear(Level level, Rect rect)
        {
            for (int x = rect.x1; x <= rect.x2; x++)
            {
                for (int y = rect.y1; y <= rect.y2; y++)
                {
                    Tile tile = level.getTile(new Location(x, y));
                    if (tile == null || !tile.blocksMovement)
                        return false;
                }
            }
            return true;
        }

        private static Rect createRect(Location doorLoc, Direction side, Feature feature)
        {
            const int maxSize = 10;
            const int minSize = 4;
            int width = Util.random.Next(minSize, maxSize);
            int height = Util.random.Next(minSize, maxSize);

            if (feature == Feature.Corridor)
            {
                if (side == Direction.West || side == Direction.East)
                {
                    height = 2;
                    width *= 3;
                }
                else
                {
                    width = 2;
                    height *= 3;
                }
            }

            int x1, y1, x2, y2;

            switch (side)
            {
                case Direction.South:
                    y1 = doorLoc.y + 1;
                    x1 = doorLoc.x - (width/2);
                    y2 = y1 + height;
                    x2 = x1 + width;
                    break;
                case Direction.North:
                    y2 = doorLoc.y - 1;
                    x2 = doorLoc.x + (width/2);
                    x1 = x2 - width;
                    y1 = y2 - height;
                    break;
                case Direction.West:
                    x2 = doorLoc.x - 1;
                    y1 = doorLoc.y - (height/2);
                    x1 = x2 - width;
                    y2 = y1 + height;
                    break;
                case Direction.East:
                    x1 = doorLoc.x + 1;
                    y1 = doorLoc.y - (height/2);
                    x2 = x1 + width;
                    y2 = y1 + height;
                    break;
                default:
                    x1 = x2 = y1 = y2 = 0;
                    break;
            }
            return new Rect(x1, y1, x2, y2);
        }

        private static void addEntranceAndExit(Level level, Rect dungeon)
        {
            //for now we set the name of the entrance tile to "entrance"
            //TODO actually use staircases
            Location entrance = new Location(0, 0);

            while (level.getTile(entrance).blocksMovement)
            {
                entrance.x = Util.random.Next(dungeon.x1, dungeon.x2);
                entrance.y = Util.random.Next(dungeon.y1, dungeon.y2);
            }

            Staircase staircase = new Staircase(true);
            level.addWalkable(staircase, entrance);
            level.entrance = entrance;
        }

        private static void addChests(Level level, Rect bounds)
        {
            const int numChests = 20;
            for (int i = 0; i < numChests; i++)
            {
                Location chestLoc = new Location(0, 0);

                while (level.getTile(chestLoc).blocksMovement)
                {
                    chestLoc.x = Util.random.Next(bounds.x1, bounds.x2);
                    chestLoc.y = Util.random.Next(bounds.y1, bounds.y2);
                }

                Chest chest = new Chest();
                level.addWalkable(chest, chestLoc);

            }
        }

        private static bool isDoorCandidate(Level level, Location location)
        {
            if (!level.getTile(location).blocksMovement)
                return false;

            Location[] neighbors =
            {
                location.getAdjLocation(Direction.North),
                location.getAdjLocation(Direction.South),
                location.getAdjLocation(Direction.West),
                location.getAdjLocation(Direction.East)
            };

            for (int i = 0; i < 4; i++)
            {
                Tile tile = level.getTile(neighbors[i]);
                if (tile != null && !tile.blocksMovement)
                    return true;
            }
            return false;
        }

        private static void initLighting(Level level,  Rect bounds)
        {
            for (int x = bounds.x1; x <= bounds.x2; x++)
            {
                for (int y = bounds.y1; y <= bounds.y2; y++)
                {
                    if (isDark(level, new Location(x, y)))
                    {
                        level.setLit(new Location(x, y), Entity.LIT_FULL_DARK);
                    }
                }
            }
        }

        private static bool isDark(Level level, Location location)
        {
            if (level.getTile(location) == null || !level.getTile(location).blocksMovement)
                return false;

            Location[] neighbors =
            {
                location.getAdjLocation(Direction.North),
                location.getAdjLocation(Direction.South),
                location.getAdjLocation(Direction.West),
                location.getAdjLocation(Direction.East),
                location.getAdjLocation(Direction.NorthWest),
                location.getAdjLocation(Direction.SouthWest),
                location.getAdjLocation(Direction.NorthEast),
                location.getAdjLocation(Direction.SouthEast),
            };

            int countBlockedNeighbors = 0;

            for (int i = 0; i < neighbors.Length; i++)
            {
                Tile tile = level.getTile(neighbors[i]);
                if (tile == null || tile.blocksMovement)
                {
                    countBlockedNeighbors++;
                }
            }

            return countBlockedNeighbors == 8;
        }
    }

    public enum Feature
    {
        Room, Corridor
    }

    public struct Rect
    {
        public int x1, y1, x2, y2;

        public Rect(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public void expand()
        {
            x1--;
            y1--;
            x2++;
            y2++;
        }

        public void compress()
        {
            x1++;
            y1++;
            x2--;
            y2--;
        }

        public void compressCorridor()
        {
            if (width() <= 3)
            {
                x1++;
                x2--;
            }
            else if (height() <= 3)
            {
                y1++;
                y2--;
            }
        }

        public int width()
        {
            return x2 - x1;
        }

        public int height()
        {
            return y2 - y1;
        }
    }
}
