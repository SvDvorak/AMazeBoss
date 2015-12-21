using System.Collections.Generic;
using UnityEngine;
using Assets;

public class UnityRoomObjects : MonoBehaviour
{
    private readonly Dictionary<TilePos, GameObject> _tiles = new Dictionary<TilePos, GameObject>();

    public void OnEnable()
    {
        Events.instance.AddListener<TilesAddedTwo>(TilesAdded);
        Events.instance.AddListener<TilesRemoved>(TilesRemoved);
    }

    public void OnDisable()
    {
        Events.instance.RemoveListener<TilesAddedTwo>(TilesAdded);
        Events.instance.RemoveListener<TilesRemoved>(TilesRemoved);
    }

    private void TilesAdded(TilesAddedTwo e)
    {
        foreach (var tile in e.Tiles)
        {
            TileAdded(tile);
        }
    }

    private void TileAdded(StandardTile tile)
    {
        var tileTemplate = TemplateLoader.Retrieve(new CompleteTileType(tile.Type, tile.Subtype));

        var randomTemplate = tileTemplate.Templates[Random.Range(0, tileTemplate.Templates.Count)];
        var templateInstance = CreateParented(tile, randomTemplate, tileTemplate.Bottom);
        _tiles.Add(tile.Position, templateInstance);
    }

    private GameObject CreateParented(
        StandardTile tile,
        GameObject tileTemplate,
        GameObject bottomTemplate)
    {
        var rotation = tile.Rotation ?? Random.Range(0, 4);
        var tileInstance = (GameObject) Instantiate(
            tileTemplate,
            tile.Position.ToV3(),
            Quaternion.AngleAxis(rotation*90, Vector3.up));
        tileInstance.transform.SetParent(transform);
        var bottomInstance = Instantiate(bottomTemplate);
        bottomInstance.transform.SetParent(tileInstance.transform, false);

        return tileInstance;
    }

    private void TilesRemoved(TilesRemoved e)
    {
        foreach (var tilePosition in e.TilePositions)
        {
            Destroy(_tiles[tilePosition]);
            _tiles.Remove(tilePosition);
        }
    }
}