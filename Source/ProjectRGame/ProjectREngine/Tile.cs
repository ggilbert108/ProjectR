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
                {TileType.Stone, new DrawTag[1]{DrawTag.Stone_Wall_1}}
            };
        }
    }

    public enum TileType
    {
        Water, Ground, Stone
    }
}
