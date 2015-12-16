using Assets.FileOperations;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string LevelPath;

	public void Start ()
    {
        FileOperations.Load(LevelPath);
	}
}
