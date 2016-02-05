using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class ConnectionSet
    {
        public string SubtypeName;
        public int Rotation;

        public ConnectionSet(string subtypeName, int rotation)
        {
            SubtypeName = subtypeName;
            Rotation = rotation;
        }

        public override string ToString()
        {
            return SubtypeName + " : " + Rotation;
        }
    }

    public class Connections : Dictionary<string, ConnectionSet>
    {
        public void ExpandUniqueConnections()
        {
            foreach (var connectionSetPair in this.ToList())
            {
                for (int i = 1; i < 4; i++)
                {
                    var connections = connectionSetPair.Key
                        .SkipAndLoop(i)
                        .Aggregate("", (current, connection) => current + connection);

                    if (!ContainsKey(connections))
                    {
                        var subtypeName = connectionSetPair.Value.SubtypeName;
                        var rotation = connectionSetPair.Value.Rotation - i % 4;
                        var newConnectionSet = new ConnectionSet(subtypeName, rotation);
                        Add(connections, newConnectionSet);
                    }
                }
            }
        }
    }

    public class WallAdjustmentSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private readonly Connections _connections = new Connections();
        private Group _tilesGroup;
        private HashSet<TilePos> _wallTiles;

        public TriggerOnEvent trigger { get { return Matcher.Maintype.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.Position; } }

        public WallAdjustmentSystem()
        {
            AddUniqueConnection("1010", "straight");
            AddUniqueConnection("0010", "end");
            AddUniqueConnection("1100", "curved");
            AddUniqueConnection("1110", "t");
            AddUniqueConnection("1111", "x");
            _connections.ExpandUniqueConnections();
        }

        private void AddUniqueConnection(string connections, string subtypeName)
        {
            _connections.Add(connections, new ConnectionSet(subtypeName, 0));
        }

        public void SetPool(Pool pool)
        {
            _tilesGroup = pool.GetGroup(Matcher.AllOf(Matcher.Tile, Matcher.Position));
        }

        public void Execute(List<Entity> entities)
        {
            var walls = _tilesGroup.GetEntities().Where(x => x.maintype.Value == MainTileType.Wall.ToString());
            _wallTiles = new HashSet<TilePos>(walls.Select(x => x.position.Value));

            foreach (var wall in walls)
            {
                UpdateAccordingToNeighbors(wall);
            }
        }

        public void UpdateAccordingToNeighbors(Entity tile)
        {
            var position = tile.position.Value;
            var connections =
                IsWall(position + new TilePos(0, 1)) +
                IsWall(position + new TilePos(1, 0)) +
                IsWall(position + new TilePos(0, -1)) +
                IsWall(position + new TilePos(-1, 0));

            if (!_connections.ContainsKey(connections))
            {
                return;
            }

            var connectionSet = _connections[connections];

            if (tile.subtype.Value != connectionSet.SubtypeName || tile.rotation.Value != connectionSet.Rotation)
            {
                tile.ReplaceSubtype(connectionSet.SubtypeName);
                tile.ReplaceRotation(connectionSet.Rotation);
            }
        }

        private string IsWall(TilePos tilePos)
        {
            return _wallTiles.Contains(tilePos) ? 1.ToString() : 0.ToString();
        }
    }
}
