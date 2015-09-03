using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Entity
    {
        private Location _location;
        private  DrawTag _drawTag;
        public string name;
        public int drawPriority;

        public Entity(DrawTag drawTag, string name)
        {
            _location = new Location(0, 0);
            this.name = name;
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
            set { _drawTag = value; }
        }
    }
}
