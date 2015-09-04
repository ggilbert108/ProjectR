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
        private Hud _hud;

        private Hero _hero;

        private EffectView _effectView;

        private InputManager _inputManager;

        public Window()
        {
            _hero = new Hero();

            _level = new Level(_hero);
            _levelView = new LevelView(_level);
            _hud = new Hud(_hero);

            DungeonGenerator.generateDungeon(_level, 100, 200);
            _level.setHeroInLevel();


            _effectView = new EffectView();

            _inputManager = new InputManager();
        }

        public void update(KeyboardState state)
        {

            EffectDescription? effectDescription = _level.getEffect();
            if (effectDescription != null)
            {
                Effect effect = EffectBuilder.buildEffect(effectDescription.Value, _level.getHeroLocation());
                _effectView.addEffect(effect);
            }
            else if(_effectView.empty())
            {
                _level.update();
                processInput(state);
            }
                
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

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, SpriteFont font, int screenWidth, int screenHeight)
        {
            _levelView.draw(spriteBatch, atlas, screenWidth, screenHeight);

            _effectView.draw(spriteBatch, atlas, screenWidth, screenHeight);

            _hud.draw(spriteBatch, font, screenWidth, screenHeight);
        }
    }
}
