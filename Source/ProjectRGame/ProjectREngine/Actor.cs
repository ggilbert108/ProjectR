using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public abstract class Actor : Entity
    {
        protected Action nextAction;

        public bool canOpenDoors;

        public int str, def, dex, hp, maxHp, speed, energy;
        protected const int ENERGY_THRESHOLD = 100;

        public Faction faction;

        protected Actor(DrawTag drawTag, string name) : base(drawTag, name)
        {
            nextAction = null;
            canOpenDoors = false;
            energy = 0;
        }

        public virtual Action getNextAction(ref ActionResult result)
        {
            if (nextAction == null)
            {
                result = ActionResult.Error;
                return null;
            }

            energy += speed;
            if (energy < ENERGY_THRESHOLD)
            {
                result = ActionResult.Wait;
                return null;
            }
            else
            {
                energy -= ENERGY_THRESHOLD;
                result = ActionResult.FetchedAction;
                Action action = nextAction;
                nextAction = null;
                return action;
            }
        }

        public void setNextAction(Action action)
        {
            if (nextAction == null)
            {
                action.bindActor(this);
                nextAction = action;
            }
        }

        public void regenHp()
        {
            if (hp > 0)
            {
                hp += healthRegen;
                if (hp > maxHp)
                    hp = maxHp;
            }

        }

        public int healthRegen
        {
            get
            {
                double percent = def/5.0;
                percent /= 100.0;
                return (int) (percent*maxHp);
            }
        }
    }

    public enum ActionResult
    {
        Error, Wait, FetchedAction, PlayerWait, DoNothing
    }

    public enum Faction
    {
        Good, Evil
    }
}
