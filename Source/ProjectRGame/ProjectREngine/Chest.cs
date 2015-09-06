using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectREngine.Items;

namespace ProjectREngine
{
    public class Chest : Entity
    {
        private bool _closed;
        public bool isTrapped;
        private Item item;

        public Chest() : base(DrawTag.Chest_Closed, "chest")
        {
            _closed = true;


            if (Util.random.NextDouble() < 0.20)
            {
                isTrapped = true;
            }
            //TODO randomly select an item to go inside the chest
            item = new StrengthPotion();
        }

        public Item takeItem()
        {
            Item i = item;
            item = null;
            return i;
        }

        public bool closed
        {
            get { return _closed; }
            set
            {
                _closed = value;
                drawTag = _closed ? DrawTag.Chest_Closed : DrawTag.Chest_Open;
            }
        }
    }
}
