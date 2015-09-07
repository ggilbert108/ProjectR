using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public static class AStarSearch
    {
        public static Location getFirstStep(Location start, Location end, Level level)
        {
            AStarNode lastStep = aStarSearch(start, end, level);
            while (lastStep.parent != null && lastStep.parent.parent != null)
            {
                lastStep = lastStep.parent;
            }

            return lastStep.location;
        }

        private static AStarNode aStarSearch(Location start, Location end, Level level)
        {
                  
            HashSet<AStarNode> open = new HashSet<AStarNode>();
            HashSet<AStarNode> closed = new HashSet<AStarNode>();

            AStarNode startNode = new AStarNode(start);
            startNode.g = 0;
            startNode.h = start.distance(end);
            open.Add(startNode);

            while (open.Count > 0)
            {
                AStarNode q = new AStarNode(new Location(0, 0));
                double min = 10000;
                foreach (AStarNode node in open)
                {
                    if (node.f() < min)
                    {
                        min = node.f();
                        q = node;
                    }
                }

                open.Remove(q);
                Direction[] directions = {Direction.North, Direction.South, Direction.East, Direction.West};

                for (int i = 0; i < 4; i++)
                {
                    Location adj = q.location.getAdjLocation(directions[i]);
                    if (level.getTile(adj).blocksMovement)
                        continue;

                    AStarNode node = new AStarNode(adj);
                    if (closed.Contains(node))
                        continue;
                    node.setParent(q);

                    if (adj.Equals(end))
                        return node;

                    node.g = q.g + 1;
                    node.h = adj.distance(end);

                    bool add = true;
                    foreach (AStarNode openNode in open)
                    {
                        if (openNode.location.Equals(node.location) && openNode.f() < node.f())
                            add = false;
                    }
                    foreach (AStarNode closedNode in closed)
                    {
                        if (closedNode.location.Equals(node.location) && closedNode.f() < node.f())
                            add = false;
                    }
                    if (add)
                    {
                        open.Add(node);
                    }
                }
                closed.Add(q);
            }
            return startNode;
        }

        class AStarNode
        {
            public AStarNode parent;
            public readonly Location location;

            public double g, h;

            public AStarNode(Location location)
            {
                this.location = location;
                g = 1000;
            }

            public void setParent(AStarNode node)
            {
                parent = node;
            }

            public double f()
            {
                return g + h;
            }

            public override int GetHashCode()
            {
                return location.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                AStarNode other = (AStarNode) obj;
                return other.location.Equals(location);
            }
        }
    }
}
