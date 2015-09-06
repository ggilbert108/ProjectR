﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Hero : Actor
    {
        private List<Item> inventory;

        public const int MAX_SIGHT_DISTANCE = 8;

        public Rect visibleRect;

        public bool justMoved;

        public Hero() : base(DrawTag.Hero, "Hero")
        {
            canOpenDoors = true;

            justMoved = false;

            inventory = new List<Item>();

            visibleRect = new Rect(0, 0, 0, 0);
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

        public override Location location
        {
            get { return _location; }
            set
            {
                _location = value;
                justMoved = true;
            }
        }
    }
}
