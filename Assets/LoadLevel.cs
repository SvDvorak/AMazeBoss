using Assets;
using Assets.FileOperations;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public static string EditorLevelPath = "";
    public string LevelPath;

	public void Start ()
    {
        if(EditorLevelPath != "")
        {
            LevelPath = EditorLevelPath;
        }

        FileOperations.Load(LevelPath);
    }
}
