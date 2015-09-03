using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Hero : Actor
    {
        private List<Item> inventory; 

        public Hero() : base(DrawTag.Hero, "Hero")
        {
            drawPriority = 0;
            canOpenDoors = true;

            inventory = new List<Item>();
        }

        public void giveItem(Item item)
        {
            inventory.Add(item);
        }

        public override Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                result = ActionResult.PlayerWait;
                return null;
            }
            else
            {
                return base.getNextAction(ref result);
            }
        }
    }
}
