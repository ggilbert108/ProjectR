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

        private const int TILE_SIZE = 32;

        private static Dictionary<DrawTag, Rectangle> imagePositions; 

        public LevelView(Level level)
        {
            _level = level;
        }

        public static void initContent()
        {
            imagePositions = new Dictionary<DrawTag, Rectangle>()
            {
                {DrawTag.Tile, new Rectangle(32 * 0, 32 * 13, 32, 32)},
                {DrawTag.Hero, new Rectangle(32 * 20, 32 * 1, 32, 32)}
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
        }
    }
}
