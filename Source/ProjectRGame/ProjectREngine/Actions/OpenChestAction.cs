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
            if (actor.canOpenDoors)
            {
                if (_chest.closed)
                {
                    _chest.closed = false;
                    if (_chest.isTrapped)
                    {
                        EffectDescription description = new EffectDescription(EffectType.Explosion, actor.location);
                        level.queueEffect(description);

                        const int chestTrapMaxDamage = 10;

                        int damage = Util.random.Next(1, chestTrapMaxDamage);
                        actor.hp -= damage;

                        MessageLog.log("The chest was trapped! " + actor.name +
                            " hit for " + damage + " damage");
                    }
                    else
                    {
                        Item chestItem = _chest.takeItem();
                        ((Hero) actor).giveItem(chestItem);
                        MessageLog.log(actor.name + " opened a chest and recieved " + chestItem.name);
                    }
                }
            }
            return true;
        }
    }
}
