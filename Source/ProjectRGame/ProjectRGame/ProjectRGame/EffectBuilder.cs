using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectREngine;
using ProjectRGame.Effects;

namespace ProjectRGame
{
    static class EffectBuilder
    {
        public static Effect buildEffect(EffectDescription description, Location heroLocation)
        {
            switch (description.type)
            {
                case EffectType.Explosion:
                    return buildExplosion(description, heroLocation);
            }
            return null;
        }

        private static Explosion buildExplosion(EffectDescription description, Location heroLocation)
        {
            Location location = description.startLocation;
            location.x -= heroLocation.x;
            location.y -= heroLocation.y;


            location.x *= LevelView.TILE_SIZE;
            location.y *= LevelView.TILE_SIZE;


            Explosion explosion = new Explosion(location);
            return explosion;
        }
    }
}
