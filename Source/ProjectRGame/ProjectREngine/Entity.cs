using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Entity
    {
        protected Location _location;
        private  DrawTag _drawTag;
        public string name;

        private int _lit;
        public bool discovered;

        public const int LIT_DIM = 126;
        public const int LIT_FULL = 0;
        public const int LIT_DARK = 230;
        public const int LIT_FULL_DARK = 240;

        public Entity(DrawTag drawTag, string name)
        {
            _location = new Location(0, 0);
            this.name = name;
            _drawTag = drawTag;

            discovered = false;
            lit = LIT_DIM;
        }

        public virtual Location location
        {
            get { return _location; }
            set { _location = value; }
        }

        public DrawTag drawTag
        {
            get { return _drawTag; }
            set { _drawTag = value; }
        }

        public int lit
        {
            get { return _lit; }
            set
            {
                _lit = value;
                if (_lit == LIT_FULL)
                {
                    discovered = true;
                }
            }
        }
    }
}
