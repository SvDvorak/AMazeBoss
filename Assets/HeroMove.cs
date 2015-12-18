using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class HeroMove : MonoBehaviour
    {
        private readonly Dictionary<KeyCode, TilePos> _moveDirections = new Dictionary<KeyCode, TilePos>
            {
                { KeyCode.UpArrow, new TilePos(0, 1) },
                { KeyCode.DownArrow, new TilePos(0, -1) },
                { KeyCode.LeftArrow, new TilePos(-1, 0) },
                { KeyCode.RightArrow, new TilePos(1, 0) }
            };

        private bool _frozen = false;

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
            _frozen = !e.CurseBoss;
        }

        public void Update()
        {
            if (_frozen)
            {
                return;
            }

            var currentPos = new TilePos(transform.position);
            var newMove = new TilePos(0, 0);

            foreach (var moveDirection in _moveDirections)
            {
                if (Input.GetKeyDown(moveDirection.Key))
                {
                    newMove = moveDirection.Value;
                }
            }

            if (RoomInfo.Instance.CanMoveTo(newMove + currentPos))
            {
                transform.Translate(newMove.ToV3(), Space.World);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _frozen = true;
                Events.instance.Raise(new CurseSwitch(false));
            }
        }
    }
}