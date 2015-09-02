using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRGame
{
    public abstract class Effect
    {
        protected System.Threading.Timer timer;
        protected DrawTag drawTag;
        protected Location coord;
        protected bool finished;

        private int EFFECT_SIZE = 32;

        protected Effect(int interval, DrawTag drawTag, Location coord)
        {
            TimerCallback callback = atInterval;

            timer = new System.Threading.Timer(callback, null, 0, interval);

            this.drawTag = drawTag;
            this.coord = coord;
            finished = false;
        }
        

        public abstract void atInterval(Object source);

        public void draw(SpriteBatch spriteBatch, Texture2D atlas, int screenWidth, int screenHeight)
        {
            Rectangle source = LevelView.imagePositions[drawTag];
            Rectangle dest = new Rectangle(coord.x, coord.y, EFFECT_SIZE, EFFECT_SIZE);

            dest.X += screenWidth/2;
            dest.Y += screenHeight/2;

            dest.X -= EFFECT_SIZE/2;
            dest.Y -= EFFECT_SIZE/2;

            spriteBatch.Draw(atlas, dest, source, Color.White);
        }

        public bool isFinished()
        {
            return finished;
        }
    }
}
