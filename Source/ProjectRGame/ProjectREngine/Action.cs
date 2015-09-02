using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Action
    {
        protected Actor actor;

        public void bindActor(Actor actor)
        {
            this.actor = actor;
        }

        public abstract bool doAction();
    }
}
