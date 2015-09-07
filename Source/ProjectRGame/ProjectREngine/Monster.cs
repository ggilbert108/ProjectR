using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectREngine.Actions;

namespace ProjectREngine
{
    public abstract class Monster : Actor
    {
        private bool _canSeePlayer;

        protected Monster(DrawTag drawTag, string name) : base(drawTag, name)
        {
            _canSeePlayer = false;
            faction = Faction.Evil;
        }

        public override Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                if (_canSeePlayer)
                {
                    setNextAction(new MoveTowardHeroAction());
                }
                else
                {
                    setNextAction(new MoveRandomlyAction());
                }
            }
            return base.getNextAction(ref result);
        }

        public bool canSeePlayer
        {
            get { return _canSeePlayer; }
            set { _canSeePlayer = value; }
        }
    }
}
