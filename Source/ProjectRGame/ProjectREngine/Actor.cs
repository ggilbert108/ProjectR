using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Actor : Entity
    {
        protected Action nextAction;

        public bool canOpenDoors;

        protected Actor(DrawTag drawTag, string name) : base(drawTag, name)
        {
            nextAction = null;

            drawPriority = 1;
            canOpenDoors = false;
        }

        public virtual Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                result = ActionResult.Error;
                return null;
            }

            result = ActionResult.FetchedAction;
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

    public enum ActionResult
    {
        Error, Wait, FetchedAction, PlayerWait, DoNothing
    }
}
