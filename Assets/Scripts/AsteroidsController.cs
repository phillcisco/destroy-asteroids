using System;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float speed;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.AddForceX(-speed, ForceMode2D.Impulse);
    }

    // void Update()
    // {
    //     transform.Rotate(Vector3.forward, 1);
    // }

    // void FixedUpdate()
    // {
    //     _rb.AddForceX(-speed*Time.deltaTime);
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
