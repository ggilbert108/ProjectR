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
            int damage = actor.str;
            damage -= (int)(Util.random.NextDouble()*_target.def);

            if (damage < 0)
                damage = 0;

            _target.hp -= damage;

            MessageLog.log(actor.name + " attacked " + _target.name + " for " + damage + " damage");

            if (_target.hp <= 0)
            {
                MessageLog.log(actor.name + " killed " + _target.name);
            }

            return true;
        }
    }
}
