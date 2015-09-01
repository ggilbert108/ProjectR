using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Entity
    {
        private Location _location;
        private readonly DrawTag _drawTag;

        public Entity(DrawTag drawTag)
        {
            _drawTag = drawTag;
        }

        public Location location
        {
            get { return _location; }
            set { _location = value; }
        }

        public DrawTag drawTag
        {
            get { return _drawTag; }
        }
    }
}
