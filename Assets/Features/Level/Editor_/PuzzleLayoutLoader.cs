using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Level.Editor_;
using UnityEngine;

namespace Assets.Features.Level.Editor_
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
                public string Type;
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
                    var properties = new Dictionary<string, object>();
                    puzzleObject.Properties.ForEach(property =>
                    {
	                    try
	                    {
							var type = Type.GetType(property.Type);
							var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
							var value = converter.ConvertFromInvariantString(property.Value);
							properties.Add(property.Key, value);
						}
						catch(Exception) { }
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
                            Properties = puzzleObject.Properties.Values.Select(x => new FlatLayout.PuzzleObject.Property()
                                {
                                    Key = x.Key,
                                    Value = x.Value.ToString(),
                                    Type = x.Type.ToString()
                                }).ToList()
                        })
                    .ToList();
        }
    }
}
