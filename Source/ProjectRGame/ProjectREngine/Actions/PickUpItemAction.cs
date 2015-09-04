using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Actions
{
    public class PickUpItemAction : Action
    {
        private Item _item;

        public PickUpItemAction(Item item)
        {
            _item = item;
        }

        public override bool doAction()
        {
            Hero hero = (Hero) actor;

            level.removeItem(_item.location);
            hero.giveItem(_item);
            hero.location = _item.location;

            MessageLog.log(hero.name + " picked up " + _item.name);
            return true;
        }
    }
}
