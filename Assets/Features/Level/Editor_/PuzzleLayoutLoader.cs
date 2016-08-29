using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Features.Level;
using UnityEngine;

namespace Assets.Level.Editor_
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

            [SerializeField]
            public List<Property> Properties = new List<Property>();

            [Serializable]
            public class Property
            {
                public string Key;
                public string Value;
            }
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
            TestLayout.Objects.ForEach(puzzleObject =>
                {
                    var properties = new Dictionary<string, string>();
                    puzzleObject.Properties.ForEach(property =>
                    {
                        properties.Add(property.Key, property.Value);
                    });
                    layout.PlaceObject(puzzleObject.Type, puzzleObject.Position, properties);
                });
        }

        private void SaveToFlatLayout()
        {
            TestLayout.Connections = PuzzleLayout.Instance.GetAllConnections();
            TestLayout.Objects =
                PuzzleLayout.Instance.GetAllObjects()
                    .Select(puzzleObject => new FlatLayout.PuzzleObject
                        {
                            Type = puzzleObject.Type,
                            Position = puzzleObject.Position,
                            Properties = puzzleObject.Properties.Select(x => new FlatLayout.PuzzleObject.Property()
                                {
                                    Key = x.Key,
                                    Value = x.Value
                                }).ToList()
                        })
                    .ToList();
        }
    }
}
