using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class TileBottomSpawner : MonoBehaviour
    {
        private List<GameObject> _detailedBottoms;
        private GameObject _emptyBottom;

        public void Start()
        {
            var bottoms = Resources.LoadAll<GameObject>("Bottoms");
            _detailedBottoms = bottoms.Where(x => x.NameContains("detail")).ToList();
            _emptyBottom = bottoms.Single(x => x.NameContains("empty"));

            Events.instance.AddListener<TileAdded>(TileAdded);
        }

        private void TileAdded(TileAdded e)
        {
            var bottomInstance = Instantiate(_emptyBottom);
            bottomInstance.transform.SetParent(e.GameObject.transform, false);
        }
    }
}
