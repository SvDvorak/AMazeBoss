using UnityEngine;

namespace Assets.LevelEditor
{
    public class ShowFileDialogs : MonoBehaviour
    {
        public GameObject SaveDialog;
        public GameObject LoadDialog;
        public GameObject ExportDialog;

        public void ShowSave()
        {
            SaveDialog.SetActive(true);
            LoadDialog.SetActive(false);
            ExportDialog.SetActive(false);
        }

        public void ShowLoad()
        {
            SaveDialog.SetActive(false);
            LoadDialog.SetActive(true);
            ExportDialog.SetActive(false);
        }

        public void ShowExport()
        {
            ExportDialog.SetActive(true);
        }
    }
}
