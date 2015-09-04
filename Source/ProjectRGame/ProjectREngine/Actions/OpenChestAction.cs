using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class OpenChestAction : Action
    {
        private Chest _chest;

        public OpenChestAction(Chest chest)
        {
            _chest = chest;
        }

        public override bool doAction()
        {
            if (_chest.closed)
            {
                _chest.closed = false;
                Item chestItem = _chest.takeItem();
                ((Hero) actor).giveItem(chestItem);
                MessageLog.log(actor.name + " opened a chest and recieved " + chestItem.name);
            }
            return true;
        }
    }
}
