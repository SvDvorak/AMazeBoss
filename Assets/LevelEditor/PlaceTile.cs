using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.LevelEditor
{
    public class PlaceTile : MonoBehaviour
    {
        public Material PreviewMaterial;

        private TileType _tileSelected;
        private GameObject _preview;

        public void Start()
        {
            NewTileTypeSelected(new TileSelected(TileType.Normal));

            Events.instance.AddListener<TileSelected>(NewTileTypeSelected);
        }

        private void NewTileTypeSelected(TileSelected e)
        {
            _tileSelected = e.TileTypeSelected;

            CreatePreview();
        }

        private void CreatePreview()
        {
            if (_preview != null)
            {
                Destroy(_preview);
                _preview = null;
            }

            _preview = Instantiate(TileLoader.Retrieve(_tileSelected));
            _preview.GetComponent<MeshRenderer>().material = PreviewMaterial;
        }

        public void Update()
        {
            var mouseTilePosition = GetMouseTilePosition();

            UpdatePreview(mouseTilePosition);

            Action<TilePos> clickAction = null;
            if (Input.GetMouseButtonDown(0))
            {
                clickAction = tilePos => RoomInfo.AddOrReplaceTile(tilePos, _tileSelected);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                clickAction = RoomInfo.RemoveTile;
            }

            if (mouseTilePosition.HasValue && clickAction != null)
            {
                clickAction(mouseTilePosition.Value);
            }
        }

        private void UpdatePreview(TilePos? mouseTilePosition)
        {
            if (mouseTilePosition.HasValue)
            {
                _preview.transform.position = mouseTilePosition.Value.ToV3();
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
    }
}