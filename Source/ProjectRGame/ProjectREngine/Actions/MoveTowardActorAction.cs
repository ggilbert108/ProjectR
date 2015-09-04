using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class MoveTowardActorAction : Action
    {
        private Actor _toward;

        public MoveTowardActorAction(Actor toward)
        {
            _toward = toward;
        }

        public override bool doAction()
        {
            if (actor is Hero && _toward is Chest)
            {
                alternate = new OpenChestAction((Chest)_toward);
                return false;
            }
            else if (!(_toward is Chest))
            {
                alternate = new AttackAction(_toward);
                return false;
            }
            return true;
        }
    }
}
