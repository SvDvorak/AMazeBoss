using Assets;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class UIInteractions : MonoBehaviour
    {
        public void Save(string path)
        {
            FileOperations.FileOperations.Save(path);
        }

        public void Load(string path)
        {
            FileOperations.FileOperations.Load(path);
        }

        public void Clear()
        {
            RoomInfo.ClearTiles();
        }
    }
}