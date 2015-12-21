using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.LevelEditor
{
    public class ItemPreview
    {
        private GameObject _previewObject;
        private readonly Material _previewMaterial;

        public ItemPreview(Material previewMaterial)
        {
            _previewMaterial = previewMaterial;
        }

        public void Recreate(MainTileType tile)
        {
            if (_previewObject != null)
            {
                GameObject.Destroy(_previewObject);
                _previewObject = null;
            }

            _previewObject = GameObject.Instantiate(TemplateLoader.Retrieve(new CompleteTileType(tile)).Templates.First());
            _previewObject.GetComponent<MeshRenderer>().material = _previewMaterial;
        }

        public void Update(TilePos position)
        {
            _previewObject.transform.position = position.ToV3();
        }
    }

    public class PlaceItem : MonoBehaviour
    {
        public Material PreviewMaterial;
        public Text PositionInfo;

        private StandardTile _tileSelected;
        private ItemPreview _preview;

        public void Start()
        {
            _preview = new ItemPreview(PreviewMaterial);
        }

        public void OnEnable()
        {
            Events.instance.AddListener<TileSelected>(NewTileTypeSelected);
        }

        public void OnDisable()
        {
            Events.instance.RemoveListener<TileSelected>(NewTileTypeSelected);
        }

        private void NewTileTypeSelected(TileSelected e)
        {
            _tileSelected = e.SelectedTile;

            _preview.Recreate(_tileSelected.Type);
        }

        public void Update()
        {
            var mouseTilePosition = GetMouseTilePosition();

            if (mouseTilePosition.HasValue)
            {
                _preview.Update(mouseTilePosition.Value);
                PositionInfo.text = string.Format("X: {0}\nY: {1}", mouseTilePosition.Value.X, mouseTilePosition.Value.Z);
            }

            Action<TilePos> clickAction = null;
            if (Input.GetMouseButtonDown(0))
            {
                clickAction = AddTile;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickAction = x => RoomInfoTwo.Instance.RemoveTiles(x);
            }

            if (mouseTilePosition.HasValue && clickAction != null)
            {
                clickAction(mouseTilePosition.Value);
            }
        }

        private void AddTile(TilePos tilePos)
        {
            _tileSelected.Copy().AddToRoom(tilePos, RoomInfoTwo.Instance);
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