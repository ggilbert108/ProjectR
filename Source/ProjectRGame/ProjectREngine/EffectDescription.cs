using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public struct EffectDescription
    {
        public EffectType type;
        public Location startLocation;

        public EffectDescription(EffectType type, Location startLocation)
        {
            this.type = type;
            this.startLocation = startLocation;
        }
    }

    public enum EffectType
    {
        Explosion
    }
}
