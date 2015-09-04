using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class AttackAction : Action
    {
        private Actor _target;

        public AttackAction(Actor target)
        {
            _target = target;
        }

        public override bool doAction()
        {
            MessageLog.log(actor.name + " attacked " + _target.name);
            return true;
        }
    }
}
