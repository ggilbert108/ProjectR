using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class ChangeLevelAction : Action
    {
        private bool _up;

        public ChangeLevelAction(bool up)
        {
            _up = up;
        }

        public override bool doAction()
        {
            LevelChange change = (_up) ? LevelChange.Up : LevelChange.Down;
            level.changeLevel(change);

            return true;
        }
    }
}
