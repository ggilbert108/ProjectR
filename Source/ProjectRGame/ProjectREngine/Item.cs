using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Item : Entity
    {
        protected Item(DrawTag drawTag, string name) : base(drawTag, name)
        {

        }

        public abstract void useItem(Actor actor);
    }
}
