using System;
using System.Linq;
using UnityEngine;

namespace Assets.Features.Level.Editor_
{
	[ExecuteInEditMode]
    public class PuzzleLayoutLoaderBehaviour : MonoBehaviour
    {
        [SerializeField]
        public FlatLayout SavedLayout;

        private readonly PuzzleLayoutLoader _loader;

        public PuzzleLayoutLoaderBehaviour()
        {
            _loader = new PuzzleLayoutLoader();
        }

        public void OnEnable()
        {
            PuzzleLayout.Instance.Clear();
            _loader.LoadFromFlatLayout(SavedLayout, PuzzleLayout.Instance);

            PuzzleLayout.Instance.LayoutChanged += SaveLayout;
        }

        public void OnDisable()
        {
            PuzzleLayout.Instance.LayoutChanged -= SaveLayout;
        }

        private void SaveLayout()
        {
            SavedLayout = _loader.SaveToFlatLayout(PuzzleLayout.Instance);
        }
    }

    public class PuzzleLayoutLoader
    {
        public void LoadFromFlatLayout(FlatLayout savedLayout, PuzzleLayout gameLayout)
        {
            savedLayout.Connections.ForEach(gameLayout.AddNodeConnections);
            savedLayout.Objects.ForEach(puzzleObject =>
            {
                var properties = new SetProperties();
                puzzleObject.Properties.ForEach(property =>
                {
                    var type = Type.GetType(property.Type);
                    var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
                    var value = converter.ConvertFromInvariantString(property.Value);
                    properties.Add(property.Key, value);
                });
                gameLayout.PlaceObject(puzzleObject.Type, puzzleObject.Position, properties);
            });
        }

        public FlatLayout SaveToFlatLayout(PuzzleLayout layoutToSave)
        {
            return new FlatLayout
            {
                Connections = layoutToSave.GetAllConnections(),
                Objects = layoutToSave.GetAllObjects()
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
                    .ToList()
            };
        }
    }
}
