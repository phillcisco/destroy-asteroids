using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float speed;

    public event Action<GameObject> OnAsteroidHit;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _rb.AddForceX(-speed, ForceMode2D.Impulse);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnAsteroidHit?.Invoke(gameObject);
    }
}