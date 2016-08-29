using System.Collections.Generic;

namespace Assets.Level.Editor_
{
    public class Node
    {
        public List<Node> Connections = new List<Node>();
        public TilePos Position;

        public Node(TilePos position)
        {
            Position = position;
        }
    }
}