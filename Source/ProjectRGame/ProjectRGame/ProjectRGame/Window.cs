using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRGame
{
    public class Window
    {
        private LevelView _levelView;

        public Window()
        {
            Level level = new Level();
            _levelView = new LevelView(level);
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            _levelView.draw(spriteBatch, atlas, screenWidth, screenHeight);
        }
    }
}
