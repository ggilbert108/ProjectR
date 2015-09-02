using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRGame
{
    class EffectView
    {
        private List<Effect> _effects;

        public EffectView()
        {
            _effects = new List<Effect>();
        }

        public void addEffect(Effect effect)
        {
            _effects.Add(effect);
        }

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            List<Effect> keepEffects = new List<Effect>();
            foreach (Effect effect in _effects)
            {
                effect.draw(spriteBatch, atlas, screenWidth, screenHeight);

                if (!effect.isFinished())
                {
                    keepEffects.Add(effect);
                }
            }

            _effects = keepEffects;
        }

        public bool empty()
        {
            return _effects.Count == 0;
        }
    }
}
