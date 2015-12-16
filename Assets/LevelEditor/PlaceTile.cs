using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.LevelEditor
{
    public class PlaceTile : MonoBehaviour
    {
        public Material PreviewMaterial;
        public Text PositionInfo;

        private MainTileType _tileSelected;
        private GameObject _preview;

        public void Start()
        {
            NewTileTypeSelected(new TileSelected(MainTileType.Normal));

            Events.instance.AddListener<TileSelected>(NewTileTypeSelected);
            Events.instance.AddListener<LoadingScene>(Unregister);
        }

        private void Unregister(LoadingScene e)
        {
            Events.instance.RemoveListener<TileSelected>(NewTileTypeSelected);
            Events.instance.RemoveListener<LoadingScene>(Unregister);
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

            _preview = Instantiate(TileLoader.Retrieve(new CompleteTileType(_tileSelected)).Templates.First());
            _preview.GetComponent<MeshRenderer>().material = PreviewMaterial;
        }

        public void Update()
        {
            var mouseTilePosition = GetMouseTilePosition();

            if (mouseTilePosition.HasValue)
            {
                UpdatePreview(mouseTilePosition.Value);
                PositionInfo.text = string.Format("X: {0}\nY: {1}", mouseTilePosition.Value.X, mouseTilePosition.Value.Z);
            }

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
            _preview.transform.position = mouseTilePosition.Value.ToV3();
        }

        private static TilePos? GetMouseTilePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hPlane = new Plane(Vector3.up, Vector3.zero);
            float distance;
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