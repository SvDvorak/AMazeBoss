using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class PlaceTile : MonoBehaviour
{
    private TileType _tileSelected = TileType.Normal;
    private readonly Dictionary<TileType, List<GameObject>> _tileTemplates = new Dictionary<TileType, List<GameObject>>();

    public void Start()
    {
        var tileTypes = TileTypeHelper.GetAsList();
        var allTiles = LoadTiles();

        foreach (var tileType in tileTypes)
        {
            var tileTypeGameObjects = allTiles
                .Where(x => x.name.ToUpper().Contains(tileType.ToString().ToUpper()))
                .Select(x => x as GameObject)
                .ToList();

            if (tileTypeGameObjects.Count == 0)
            {
                Debug.LogWarning("No tiles available for " + tileType);
            }

            _tileTemplates.Add(tileType, tileTypeGameObjects);
        }

        Events.instance.AddListener<TileSelected>(NewTileTypeSelected);
    }

    private static Object[] LoadTiles()
    {
        try
        {
            var allLoaded = Resources.LoadAll("Tiles");
            return (Object[]) allLoaded;
        }
        catch (Exception)
        {
            throw new Exception("Cannot load anything from Resources/Tiles");
        }
    }

    private void NewTileTypeSelected(TileSelected e)
    {
        _tileSelected = e.TileTypeSelected;
    }

    public void Update()
    {
        Action<TilePos> clickAction = null;
        if (Input.GetMouseButtonDown(0))
        {
            clickAction = AddOrReplaceTile;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            clickAction = RoomInfo.RemoveTile;
        }

        var mouseTilePosition = GetMouseTilePosition();
        if (mouseTilePosition.HasValue && clickAction != null)
        {
            clickAction(mouseTilePosition.Value);
        }
    }

    private static TilePos? GetMouseTilePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (hPlane.Raycast(ray, out distance))
        {
            var planePos = ray.GetPoint(distance);
            var tilePos = new TilePos(planePos);

            return tilePos;
        }

        return null;
    }

    private void AddOrReplaceTile(TilePos tilePos)
    {
        var tiles = _tileTemplates[_tileSelected];
        if (tiles.Count == 0)
        {
            throw new Exception("Cannot place tile, no template available for " + _tileSelected);
        }

        var tileTemplate = tiles[Random.Range(0, tiles.Count)];
        var tileInstance = (GameObject)Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4)*90, Vector3.up));
        RoomInfo.AddOrReplaceTile(tilePos, tileInstance);
    }
}