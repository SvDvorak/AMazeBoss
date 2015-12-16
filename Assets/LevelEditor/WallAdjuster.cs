using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class WallAdjuster : MonoBehaviour
    {
        public void Start()
        {
            Events.instance.AddListener<TileAdded>(AdjustWalls);
        }

        private void AdjustWalls(TileAdded e)
        {
            if (e.TileType != TileType.Wall)
            {
                return;
            }

            var copiedTileInfos = new Dictionary<TilePos, TileInfo>(RoomInfo.GetAllTiles());
            foreach (var tile in copiedTileInfos)
            {
                if (tile.Value.TileType.Main == TileType.Wall)
                {
                    AdjustAccordingToNeighbors(tile.Key, tile.Value);
                }
            }

            //var toCheck = new Queue<TilePos>();
            //toCheck.Enqueue(e.TilePos);
            //_alreadyChecked = new HashSet<TilePos>();
            //AdjustWall(toCheck);
        }

        private void AdjustAccordingToNeighbors(TilePos tilePos, TileInfo tileInfo)
        {
            var front = IsWall(tilePos + new TilePos(0, 1));
            var back = IsWall(tilePos + new TilePos(0, -1));
            var left = IsWall(tilePos + new TilePos(-1, 0));
            var right = IsWall(tilePos + new TilePos(1, 0));

            var straightType = new TileTypeA(TileType.Wall, "straight");
            var rotation = tileInfo.GameObject.transform.rotation.eulerAngles.y;

            if (front && back)
            {
                if (!tileInfo.TileType.Equals(straightType) || Math.Abs(rotation) > 0.001f)
                {
                    RoomInfo.AddOrReplaceTile(tilePos, straightType, 0);
                }
            }

            if (left && right)
            {
                if (!tileInfo.TileType.Equals(straightType) || Math.Abs(rotation - 90) > 0.001f)
                {
                    RoomInfo.AddOrReplaceTile(tilePos, straightType, 90);
                }
            }
        }

        private bool IsWall(TilePos tilePos)
        {
            return RoomInfo.HasTileAt(tilePos, TileType.Wall);
        }

        //private HashSet<TilePos> _alreadyChecked;

        //private void AdjustWall(Queue<TilePos> toCheck)
        //{
        //    if (!toCheck.Any())
        //    {
        //        return;
        //    }

        //    var current = toCheck.Dequeue();
        //    _alreadyChecked.Add(current);

        //    var adjacentPos = new TilePos(current.X + 1, current.Z);
        //    if (RoomInfo.HasTileAt(adjacentPos, TileType.Wall))
        //    {
        //        var nextCheck = new Queue<TilePos>();
        //        nextCheck.Enqueue(adjacentPos);
        //        AdjustWall(nextCheck);
        //    }
        //}
    }
}
