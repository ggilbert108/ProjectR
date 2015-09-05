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
            Rect innerRoom = createRect(center, Direction.Left, Feature.Room);
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
        }

        private static void tryAddFeature(Level level, Location doorLoc, Feature feature)
        {
            Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

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
                if (side == Direction.Left || side == Direction.Right)
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
                case Direction.Down:
                    y1 = doorLoc.y + 1;
                    x1 = doorLoc.x - (width/2);
                    y2 = y1 + height;
                    x2 = x1 + width;
                    break;
                case Direction.Up:
                    y2 = doorLoc.y - 1;
                    x2 = doorLoc.x + (width/2);
                    x1 = x2 - width;
                    y1 = y2 - height;
                    break;
                case Direction.Left:
                    x2 = doorLoc.x - 1;
                    y1 = doorLoc.y - (height/2);
                    x1 = x2 - width;
                    y2 = y1 + height;
                    break;
                case Direction.Right:
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

        private static bool isDoorCandidate(Level level, Location location)
        {
            if (!level.getTile(location).blocksMovement)
                return false;

            Location[] neighbors =
            {
                location.getAdjLocation(Direction.Up),
                location.getAdjLocation(Direction.Down),
                location.getAdjLocation(Direction.Left),
                location.getAdjLocation(Direction.Right)
            };

            for (int i = 0; i < 4; i++)
            {
                Tile tile = level.getTile(neighbors[i]);
                if (tile != null && !tile.blocksMovement)
                    return true;
            }
            return false;
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
