using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Actor : Entity
    {
        protected Action nextAction;

        protected Actor(DrawTag drawTag) : base(drawTag)
        {
            nextAction = null;
        }

        public Action getNextAction(ref bool actionReturned)
        {
            if (nextAction == null)
            {
                actionReturned = false;
                return null;
            }

            actionReturned = true;
            Action action = nextAction;
            nextAction = null;
            return action;
        }

        public void setNextAction(Action action)
        {
            if (nextAction == null)
            {
                action.bindActor(this);
                nextAction = action;
            }
        }
    }
}
