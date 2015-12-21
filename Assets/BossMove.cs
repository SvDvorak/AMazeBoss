using System.Linq;
using Assets.EntitasRefactor;
using Assets.LevelEditor;
using UnityEngine;

namespace Assets
{
    public class BossMove : MonoBehaviour
    {
        private GameObject _hero;
        private float _timeTillMove;
        private bool _frozen;

        private readonly MovementCalculator _movementCalculator;
        private MovementCalculation _currentMovePlan;

        public BossMove()
        {
            _movementCalculator = new MovementCalculator();
        }

        public void Start ()
        {
            _frozen = true;
            _timeTillMove = 1;
        }

        public void OnEnable()
        {
            Events.instance.AddListener<CurseSwitch>(SetCurse);
        }

        public void OnDisable()
        {
            Events.instance.RemoveListener<CurseSwitch>(SetCurse);
        }

        private void SetCurse(CurseSwitch e)
        {
            _frozen = e.CurseBoss;
        }

        public void Update ()
        {
            if (EditorSetup.IsInEditor || _frozen)
            {
                return;
            }

            var targetPosition = GetHeroPosition();
            if (_currentMovePlan != null && _currentMovePlan.NextStep() == targetPosition)
            {
                _frozen = true;
                Events.instance.Raise(new CurseSwitch(true));
            }

            if (_timeTillMove <= 0)
            {
                var currentPosition = new TilePos(transform.position);
                _currentMovePlan = _movementCalculator.CalculateMoveToTarget(currentPosition, targetPosition);

                if (_currentMovePlan.Successful)
                {
                    transform.position = _currentMovePlan.NextStep().ToV3();
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