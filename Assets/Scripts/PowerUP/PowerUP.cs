using System;
using UnityEngine;
using Utils;

public class PowerUP : MonoBehaviour
{

    [SerializeField] Arma arma;
    [SerializeField] float powerUpSpeed;
    
    Rigidbody2D _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.AddForceX(-powerUpSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            other.GetComponentInParent<SpaceShip>().SetArma(arma);
        }
    }
}