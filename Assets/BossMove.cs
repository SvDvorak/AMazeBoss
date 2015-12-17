using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor;
using UnityEngine;

namespace Assets
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> list, T item)
        {
            return list.Concat(new[] { item });
        }
    }

    public class BossMove : MonoBehaviour
    {
        private GameObject _hero;
        private float _timeTillMove;

        public void Start ()
        {
            _timeTillMove = 1;
        }
	
        public void Update ()
        {
            if (EditorSetup.IsInEditor)
            {
                return;
            }

            if (_timeTillMove <= 0)
            {
                var currentPosition = new TilePos(transform.position);
                var path = CalculateMoveToHero(currentPosition);

                if (path.Item2.Count > 1)
                {
                    var move = (path.Item2.Skip(1).First() - currentPosition);

                    if (RoomInfo.CanMoveTo(currentPosition + move))
                    {
                        transform.Translate(move.ToV3(), Space.World);
                    }
                }

                _timeTillMove += 0.2f;
            }

            _timeTillMove -= Time.deltaTime;
        }

        private Tuple<int, List<TilePos>> CalculateMoveToHero(TilePos currentPosition)
        {
            var heroPosition = GetHeroPosition();
            Tuple<int, List<TilePos>> victoryPath = new Tuple<int, List<TilePos>>();

            var moveDirections = new List<TilePos>()
                    {
                        new TilePos(0, 1),
                        new TilePos(0, -1),
                        new TilePos(-1, 0),
                        new TilePos(1, 0),
                    };

            var visited = new HashSet<TilePos>();
            var positionsToCheck = new List<Tuple<int, List<TilePos>>>();
            positionsToCheck.Add(new Tuple<int, List<TilePos>>(0, new List<TilePos>() { currentPosition }));
            while (positionsToCheck.Count > 0)
            {
                var path = positionsToCheck[0];
                positionsToCheck.RemoveAt(0);
                var pos = path.Item2[path.Item2.Count-1];

                if (pos == heroPosition)
                {
                    victoryPath = path;
                    break;
                }

                foreach (var moveDirection in moveDirections)
                {
                    var move = pos + moveDirection;
                    if (RoomInfo.CanMoveTo(move) && !visited.Contains(move))
                    {
                        var cost = (heroPosition - move).Length();
                        positionsToCheck.Add(new Tuple<int, List<TilePos>>(cost, path.Item2.Concat(move).ToList()));
                    }
                }

                visited.Add(pos);
                positionsToCheck = positionsToCheck.OrderBy(x => x.Item1).ToList();
            }

            return victoryPath;
        }

        public struct Tuple<T1, T2>
        {
            public readonly T1 Item1;
            public readonly T2 Item2;

            public Tuple(T1 item1, T2 item2)
            {
                Item2 = item2;
                Item1 = item1;
            }
        }

        private TilePos GetHeroPosition()
        {
            if(_hero == null)
            {
                _hero = GameObject.Find("hero_actual");
            }

            return new TilePos(_hero.transform.position);
        }
    }
}