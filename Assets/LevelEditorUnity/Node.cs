using System.Collections.Generic;

namespace Assets.LevelEditorUnity
{
    public class Node
    {
        public List<Node> Connections { get; private set; }
        public TilePos Position { get; private set; }

        public Node(TilePos position)
        {
            Connections = new List<Node>();
            Position = position;
        }
    }
}