using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Door : Entity
    {
        private bool _closed;

        public Door() : base(DrawTag.Door_Closed, "Door")
        {
            _closed = true;
            drawPriority = 2;
        }

        public bool closed
        {
            get { return _closed; }
            set
            {
                _closed = value;
                drawTag = _closed ? DrawTag.Door_Closed : DrawTag.Door_Open;
            }
        }
   }
}
