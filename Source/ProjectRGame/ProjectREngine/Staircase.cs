using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Staircase : Entity
    {
        private bool _goesUp;
        public Staircase(bool up) : base(DrawTag.Stair_Down, "stairs")
        {
            goesUp = up;
        }

        public bool goesUp
        {
            get { return _goesUp; }
            set
            {
                _goesUp = value;
                if (_goesUp)
                {
                    drawTag = DrawTag.Stair_Up;
                }
                else
                {
                    drawTag = DrawTag.Stair_Down;
                }
            }
        }
    }
}
