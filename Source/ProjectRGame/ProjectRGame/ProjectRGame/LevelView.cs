using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRGame
{
    class LevelView
    {
        private Level _level;

        public const int TILE_SIZE = 32;

        public static Dictionary<DrawTag, Rectangle> imagePositions; 

        public LevelView(Level level)
        {
            _level = level;
        }

        public static void initContent()
        {
            imagePositions = new Dictionary<DrawTag, Rectangle>()
            {
                {DrawTag.Tile_Ground_1, new Rectangle(32 * 0, 32 * 13, 32, 32)},
                {DrawTag.Tile_Ground_2, new Rectangle(32 * 1, 32 * 13, 32, 32)},
                {DrawTag.Hero, new Rectangle(32 * 20, 32 * 1, 32, 32)},
                {DrawTag.Explosion_1, new Rectangle(32 * 20, 32 * 10, 32, 32)},
                {DrawTag.Explosion_2, new Rectangle(32 * 21, 32 * 10, 32, 32)},
                {DrawTag.Explosion_3, new Rectangle(32 * 22, 32 * 10, 32, 32)},
                {DrawTag.Door_Closed, new Rectangle(32 * 23, 32 * 11, 32, 32)},
                {DrawTag.Door_Open, new Rectangle(32 * 27, 32 * 11, 32, 32)},
                {DrawTag.Strength_Potion, new Rectangle(32 * 0, 32 * 25, 32, 32)},
                {DrawTag.Chest_Closed, new Rectangle(32 * 44, 32 * 45, 32, 32)},
                {DrawTag.Chest_Open, new Rectangle(32 * 45, 32 * 45, 32, 32)},
                {DrawTag.Dungeon_Wall_1, new Rectangle(32 * 16, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_2, new Rectangle(32 * 17, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_3, new Rectangle(32 * 18, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_4, new Rectangle(32 * 19, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_5, new Rectangle(32 * 20, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_6, new Rectangle(32 * 21, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Wall_7, new Rectangle(32 * 22, 32 * 16, 32, 32)},
                {DrawTag.Dungeon_Floor_1, new Rectangle(32 * 41, 32 * 12, 32, 32)},
                {DrawTag.Stair_Up, new Rectangle(32 * 5, 32 * 45, 32, 32)},
                {DrawTag.Stair_Down, new Rectangle(32 * 4, 32 * 45, 32, 32)},
            };
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            int screenVertTiles = screenHeight/TILE_SIZE;
            int screenHorizTiles = screenWidth/TILE_SIZE;

            Location heroLocation = _level.getHeroLocation();

            int x1 = heroLocation.x - (screenHorizTiles / 2);
            int x2 = heroLocation.x + (screenHorizTiles / 2);
            int y1 = heroLocation.y - (screenVertTiles / 2);
            int y2 = heroLocation.y + (screenVertTiles / 2);

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    Location location = new Location(x, y);

                    Location offset = new Location(x - x1, y - y1);

                    List<Entity> entities = _level.getEntities(location);
                    foreach (Entity entity in entities)
                    {
                        drawEntity(spriteBatch, atlas, entity, offset);

                    }
                }
            }
        }

        private void drawEntity(SpriteBatch spriteBatch, Texture2D atlas, Entity entity, Location screenLocation)
        {
            Rectangle source = imagePositions[entity.drawTag];
            Rectangle dest = new Rectangle(screenLocation.x * TILE_SIZE, screenLocation.y * TILE_SIZE, TILE_SIZE, TILE_SIZE);

            spriteBatch.Draw(atlas, dest, source, Color.White);

            int alpha = entity.lit;
            Color color = new Color(0, 0, 0, alpha);

            spriteBatch.Draw(Game1.alphaOverlay, dest, color);
        }
    }
}
