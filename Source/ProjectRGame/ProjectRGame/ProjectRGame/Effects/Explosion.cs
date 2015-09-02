using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ProjectREngine;

namespace ProjectRGame.Effects
{
    internal class Explosion : Effect
    {
        private readonly DrawTag[] _sprites = {DrawTag.Explosion_1, DrawTag.Explosion_2, DrawTag.Explosion_3};
        private int _curSprite;

        public Explosion(Location coord) : base(50, DrawTag.Explosion_1, coord)
        {
            _curSprite = 0;
        }

        public override void atInterval(object source)
        {
            _curSprite++;

            if (_curSprite >= _sprites.Length)
            {
                finished = true;
            }
            else
            {
                drawTag = _sprites[_curSprite];
            }
        }
    }
}
