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
            carveRoom(level, dungeonRect, true, TileType.Dungeon_Wall);

            Rect innerRoom = new Rect(5, 5, 7, 10);
            carveRoom(level, innerRoom, false, TileType.Dungeon_Floor);

            addEntranceAndExit(level, dungeonRect);
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
            level.setWalkable(staircase, entrance);
            level.entrance = entrance;
        }
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
    }
}
