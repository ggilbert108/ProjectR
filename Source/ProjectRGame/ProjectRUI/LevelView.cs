using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRUI
{
    class LevelView
    {
        private Level _level;

        private const int TILE_SIZE = 32;

        public LevelView(Level level)
        {
            _level = level;
        }

        public void drawLevel(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            
        }
    }
}
