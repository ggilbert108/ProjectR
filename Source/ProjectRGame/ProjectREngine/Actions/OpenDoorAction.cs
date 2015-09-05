using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class OpenDoorAction : Action
    {
        private Door _door;

        public OpenDoorAction(Door door)
        {
            _door = door;
        }

        public override bool doAction()
        {
            if (_door.closed)
            {
                _door.closed = false;
                MessageLog.log("Door opened");
                level.updatePlayerVision();
            }
            else
            {
                Location newLocation = _door.location;
                actor.location = newLocation;
            }
            return true;
        }
    }
}
