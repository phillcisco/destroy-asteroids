using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float intensityRate;
    
    Material _mat;
    Rigidbody2D _rb;
    float _intensity = 1;
    int _intensityDir;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    void Start()
    {
        _rb.AddForceX(speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        BlinkProjectileVFX();
    }

    void BlinkProjectileVFX()
    {
        _intensity += intensityRate * Time.deltaTime * _intensityDir;
        _mat.color = new Color(1*_intensity,1*_intensity,1*_intensity);
        if (_intensity >= 3) _intensityDir = -1;
        if (_intensity <= 1) _intensityDir = 1;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}