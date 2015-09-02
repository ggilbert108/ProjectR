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
        private Level _level;
        private LevelView _levelView;

        public Window()
        {
            _level = new Level();
            _levelView = new LevelView(_level);
        }

        public void update()
        {
            _level.update();
        }

        public void queueHeroAction(ProjectREngine.Action action)
        {
            _level.hero.setNextAction(action);
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            _levelView.draw(spriteBatch, atlas, screenWidth, screenHeight);
        }
    }
}
