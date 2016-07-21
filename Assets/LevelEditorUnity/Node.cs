using System.Collections.Generic;

namespace Assets.LevelEditorUnity
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