using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Tile : Entity
    {
        public readonly bool blocksMovement;

        private static Dictionary<TileType, DrawTag[]> tileMap; 

        public Tile(bool blocksMovement, TileType type) : base(DrawTag.Tile_Ground_1, "")
        {
            this.blocksMovement = blocksMovement;
            setDrawTag(type);
            discovered = true;
        }

        private void setDrawTag(TileType type)
        {
            if (tileMap == null)
            {
                initTileMap();
            }
            DrawTag[] possible = tileMap[type];
            drawTag = Util.chooseRandomElement(possible);
        }

        private static void initTileMap()
        {
            tileMap = new Dictionary<TileType, DrawTag[]>()
            {
                {TileType.Ground, new DrawTag[2]{DrawTag.Tile_Ground_1, DrawTag.Tile_Ground_2}},
                {TileType.Dungeon_Wall, new DrawTag[7]{DrawTag.Dungeon_Wall_1, DrawTag.Dungeon_Wall_2, DrawTag.Dungeon_Wall_3, DrawTag.Dungeon_Wall_4, DrawTag.Dungeon_Wall_5, DrawTag.Dungeon_Wall_6, DrawTag.Dungeon_Wall_7}},
                {TileType.Dungeon_Floor, new DrawTag[1]{DrawTag.Dungeon_Floor_1}},
            };
        }
    }

    public enum TileType
    {
        Water, Ground, Dungeon_Wall, Dungeon_Floor
    }
}
