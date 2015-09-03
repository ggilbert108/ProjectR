using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine.Items
{
    public class StrengthPotion : Item
    {
        public StrengthPotion() : base(DrawTag.Strength_Potion, "Strength Potion")
        {

        }

        public override void useItem(Actor actor)
        {
            Console.WriteLine("A strength potion was used");
        }
    }
}
