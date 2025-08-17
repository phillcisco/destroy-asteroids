
using System;
using UnityEngine;


public class Shredder : MonoBehaviour
{

    public static Shredder Instance;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public event Action<GameObject> OnAsteroidHitShredder;
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Projectile"))
    //     {
    //         //OnAsteroidHitShredder?.Invoke(other.transform.parent.gameObject);
    //         ProjectilesPool.Instance.ReturnToPool(,ot);
    //     }
    //
    // }
}
