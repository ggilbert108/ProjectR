using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectREngine;
using ProjectREngine.Actions;
using LevelChange = ProjectREngine.LevelChange;

namespace ProjectRGame
{
    public class Window
    {
        private volatile Level _level;
        private LevelView _levelView;
        private Hud _hud;

        private Hero _hero;

        private EffectView _effectView;

        private InputManager _inputManager;

        public Window()
        {
            _hero = new Hero();

            _level = new Level(_hero, true);
            _levelView = new LevelView(_level);
            _hud = new Hud(_hero);

            DungeonGenerator.generateDungeon(_level, 200, 200);
            _level.setHeroInLevel(false);

            _effectView = new EffectView();

            _inputManager = new InputManager();
        }

        /// <summary>
        /// Updates the game engine and processes any effects
        /// </summary>
        /// <param name="keyState"></param>
        /// <returns>Returns true if the game should cease it's update loop</returns>
        public bool update(KeyboardState keyState)
        {
            if (_level.levelChange != LevelChange.None)
            {
                if (_level.levelChange == LevelChange.Down)
                {
                    _level.levelChange = LevelChange.None;
                    _level = _level.downLevel;
                    _level.setHeroInLevel(false);
                }
                else
                {
                    _level.levelChange = LevelChange.None;
                    _level = _level.upLevel;
                    _level.setHeroInLevel(true);
                }
                _levelView = new LevelView(_level);
                return true;
            }

            EffectDescription? effectDescription = _level.getEffect();
            if (effectDescription != null)
            {
                Effect effect = EffectBuilder.buildEffect(effectDescription.Value, _level.getHeroLocation());
                _effectView.addEffect(effect);
                return true;
            }

            if (_effectView.empty())
            {
                processInput(keyState);
                return _level.update();
            }
            return true;
        }

        private void processInput(KeyboardState state)
        {
            _inputManager.update(state);

            Hero hero = _level.hero;

            if (_inputManager.keyTyped(Keys.Up))
            {
                hero.setNextAction(new MoveAction(Direction.North));
            }
            else if (_inputManager.keyTyped(Keys.Down))
            {
                hero.setNextAction(new MoveAction(Direction.South));
            }
            else if (_inputManager.keyTyped(Keys.Left))
            {
                hero.setNextAction(new MoveAction(Direction.West));
            }
            else if (_inputManager.keyTyped(Keys.Right))
            {
                hero.setNextAction(new MoveAction(Direction.East));
            }
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            if (_level.gameOver)
            {
                spriteBatch.DrawString(Game1.bigFont, "GAME OVER", new Vector2(50, 50), Color.Red);
            }
            else
            {
                _levelView.draw(spriteBatch, atlas, screenWidth, screenHeight);

                _effectView.draw(spriteBatch, atlas, screenWidth, screenHeight);

                _hud.draw(spriteBatch, screenWidth, screenHeight);
            }
        }
    }
}
