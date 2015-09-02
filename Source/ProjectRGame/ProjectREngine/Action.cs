using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Action
    {
        protected Actor actor;
        protected Level level;

        public void bindActor(Actor actor)
        {
            this.actor = actor;
        }

        public void bindLevel(Level level)
        {
            this.level = level;
        }

        public abstract bool doAction();
    }
}
