using UnityEngine;
using System.Collections;

public class ScrollingUVs : MonoBehaviour
{
    public int MaterialIndex = 0;
    public Vector2 UvAnimationRate = new Vector2(1.0f, 0.0f);
    public string TextureName = "_MainTex";

    private Vector2 _uvOffset = Vector2.zero;
    private Renderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void LateUpdate()
    {
        _uvOffset += (UvAnimationRate * Time.deltaTime);
        if (_renderer.enabled)
        {
            _renderer.materials[MaterialIndex].SetTextureOffset(TextureName, _uvOffset);
        }
    }
}