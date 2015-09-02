using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectREngine;

namespace ProjectRGame
{
    public class Window
    {
        private Level _level;
        private LevelView _levelView;

        private InputManager _inputManager;

        public Window()
        {
            _level = new Level();
            _levelView = new LevelView(_level);

            _inputManager = new InputManager();
        }

        public void update(KeyboardState state)
        {
            _level.update();
                
            processInput(state);
        }

        private void processInput(KeyboardState state)
        {
            _inputManager.update(state);

            Hero hero = _level.hero;

            if (_inputManager.keyTyped(Keys.Up))
            {
                hero.setNextAction(new MoveAction(Direction.Up));
            }
            else if (_inputManager.keyTyped(Keys.Down))
            {
                hero.setNextAction(new MoveAction(Direction.Down));
            }
            else if (_inputManager.keyTyped(Keys.Left))
            {
                hero.setNextAction(new MoveAction(Direction.Left));
            }
            else if (_inputManager.keyTyped(Keys.Right))
            {
                hero.setNextAction(new MoveAction(Direction.Right));
            }
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            _levelView.draw(spriteBatch, atlas, screenWidth, screenHeight);
        }
    }
}
