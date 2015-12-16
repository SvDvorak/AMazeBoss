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

    public class WallAdjuster : MonoBehaviour
    {
        private Dictionary<string, ConnectionSet> _connectionSets = new Dictionary<string, ConnectionSet>();
        private bool _isAdjusting;

        public void Start()
        {
            Events.instance.AddListener<TileAdded>(AdjustWalls);

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

        private void AdjustWalls(TileAdded e)
        {
            if (_isAdjusting || e.TileType != MainTileType.Wall)
            {
                return;
            }

            _isAdjusting = true;

            var copiedTileInfos = new Dictionary<TilePos, TileInfo>(RoomInfo.GetAllTiles());
            foreach (var tile in copiedTileInfos)
            {
                if (tile.Value.TileType.Main == MainTileType.Wall)
                {
                    AdjustAccordingToNeighbors(tile.Key, tile.Value);
                }
            }

            _isAdjusting = false;
        }

        private void AdjustAccordingToNeighbors(TilePos tilePos, TileInfo tileInfo)
        {
            var connections = IsWall(tilePos + new TilePos(0, 1)) +
                IsWall(tilePos + new TilePos(1, 0)) +
                IsWall(tilePos + new TilePos(0, -1)) +
                IsWall(tilePos + new TilePos(-1, 0));

            if (!_connectionSets.ContainsKey(connections))
            {
                return;
            }

            var connectionSet = _connectionSets[connections];

            var tileTransform = tileInfo.GameObject.transform;
            if (tileInfo.TileType.Subtype != connectionSet.SubtypeName)
            {
                RoomInfo.AddOrReplaceTile(tilePos, new CompleteTileType(MainTileType.Wall, connectionSet.SubtypeName), connectionSet.Rotation * 90);
            }
            else if (Math.Abs((int) (tileTransform.rotation.eulerAngles.y - connectionSet.Rotation*90)) > 0.001f)
            {
                tileTransform.rotation = Quaternion.Euler(0, connectionSet.Rotation*90, 0);
            }
        }

        private string IsWall(TilePos tilePos)
        {
            return (RoomInfo.HasTileAt(tilePos, MainTileType.Wall) ? 1 : 0).ToString();
        }
    }
}