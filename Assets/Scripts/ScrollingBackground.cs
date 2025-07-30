using System;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] float scrollingSpeed;

    Material _mat;
    float _offset;

    void Awake()
    {
        _mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground()
    {
        _offset += scrollingSpeed * Time.deltaTime;
        _mat.mainTextureOffset = new Vector2(_offset%1.0000000f, 0);
    }
}
