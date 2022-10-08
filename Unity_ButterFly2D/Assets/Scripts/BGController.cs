using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGController : MonoBehaviour
{
    private MeshRenderer _render;
    private float _offset;
    [SerializeField] float _speed;
    void Start()
    {
        _render = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _offset += Time.deltaTime * _speed;
        _render.material.mainTextureOffset = new Vector2(_offset, 0);
    }
}
