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
        private readonly MovementCalculator _movementCalculator;

        public BossMove()
        {
            _movementCalculator = new MovementCalculator();
        }

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
                var movementCalculation = _movementCalculator.CalculateMoveToHero(currentPosition, GetHeroPosition());

                if (movementCalculation.Successful)
                {
                    transform.position = movementCalculation.Path.First().ToV3();
                }

                _timeTillMove += 0.2f;
            }

            _timeTillMove -= Time.deltaTime;
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