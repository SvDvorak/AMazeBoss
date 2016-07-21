using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.LevelEditorUnity
{
    [Serializable]
    public class FlatLayout
    {
        [Serializable]
        public class PuzzleObject
        {
            [SerializeField]
            public string Type;
            [SerializeField]
            public TilePos Position;
        }

        [SerializeField]
        public List<PuzzleObject> Objects;

        [SerializeField]
        public List<NodeConnection> Connections;
    }

    [ExecuteInEditMode]
    public class PuzzleLayoutLoader : MonoBehaviour
    {
        [SerializeField]
        public FlatLayout TestLayout;

        public void OnEnable()
        {
            PuzzleLayout.Instance.Clear();
            LoadFromFlatLayout(PuzzleLayout.Instance);
            PuzzleLayout.Instance.LayoutChanged += SaveToFlatLayout;
        }

        public void OnDisable()
        {
            PuzzleLayout.Instance.LayoutChanged -= SaveToFlatLayout;
        }

        private void LoadFromFlatLayout(PuzzleLayout layout)
        {
            TestLayout.Connections.ForEach(layout.AddNodeConnections);
            TestLayout.Objects.ForEach(x => layout.PlaceObject(x.Type, x.Position));
        }

        private void SaveToFlatLayout()
        {
            TestLayout.Connections = PuzzleLayout.Instance.GetAllConnections();
            TestLayout.Objects =
                PuzzleLayout.Instance.GetAllObjects()
                    .SelectMany(x => x.Value
                        .Select(y => new FlatLayout.PuzzleObject { Type = x.Key, Position = y }))
                    .ToList();
        }
    }
}
