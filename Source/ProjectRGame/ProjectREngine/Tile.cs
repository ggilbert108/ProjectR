using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Tile : Entity
    {
        public readonly bool blocksMovement;

        public Tile(bool blocksMovement) : base(DrawTag.Tile)
        {
            this.blocksMovement = blocksMovement;
        }
    }
}
