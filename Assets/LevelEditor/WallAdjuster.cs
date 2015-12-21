using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public class WallAdjuster
    {
        private readonly Dictionary<string, ConnectionSet> _connectionSets = new Dictionary<string, ConnectionSet>();

        public WallAdjuster()
        {
            _connectionSets.Add("1010", new ConnectionSet("straight", 0));
            _connectionSets.Add("1000", new ConnectionSet("straight", 0));
            _connectionSets.Add("1100", new ConnectionSet("curved", 0));
            ExpandUniqueConnections();
        }

        private void ExpandUniqueConnections()
        {
            foreach (var connectionSetPair in _connectionSets.ToList())
            {
                for (int i = 1; i < 4; i++)
                {
                    var connections = connectionSetPair.Key
                        .SkipAndLoop(i)
                        .Aggregate("", (current, connection) => current + connection);

                    if (!_connectionSets.ContainsKey(connections))
                    {
                        var subtypeName = connectionSetPair.Value.SubtypeName;
                        var rotation = connectionSetPair.Value.Rotation-i%4;
                        var newConnectionSet = new ConnectionSet(subtypeName, rotation);
                        _connectionSets.Add(connections, newConnectionSet);
                    }
                }
            }
        }

        //private void AdjustWalls(TilesAddedTwo e)
        //{
        //    if (_isAdjusting || e.Tiles.Any(x => x.Type != MainTileType.Wall))
        //    {
        //        return;
        //    }

        //    _isAdjusting = true;

        //    var copiedTileInfos = RoomInfoTwo.Instance.GetAllTiles();
        //    foreach (var tile in copiedTileInfos)
        //    {
        //        if (tile.Type == MainTileType.Wall)
        //        {
        //            AdjustAccordingToNeighbors(tile);
        //        }
        //    }

        //    _isAdjusting = false;
        //}

        public void UpdateAccordingToNeighbors(WallTile tile)
        {
            var connections =
                IsWall(tile.Position + new TilePos(0, 1)) +
                IsWall(tile.Position + new TilePos(1, 0)) +
                IsWall(tile.Position + new TilePos(0, -1)) +
                IsWall(tile.Position + new TilePos(-1, 0));

            if (!_connectionSets.ContainsKey(connections))
            {
                return;
            }

            var connectionSet = _connectionSets[connections];

            tile.Subtype = connectionSet.SubtypeName;
            tile.Rotation = connectionSet.Rotation;
        }

        private string IsWall(TilePos tilePos)
        {
            return (RoomInfoTwo.Instance.HasTileAt(tilePos, MainTileType.Wall) ? 1 : 0).ToString();
        }
    }
}