using System;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Collider do shredder: " + other.gameObject);
        if(other.transform.parent.CompareTag("Asteroid"))
            other.GetComponentInParent<AsteroidsController>().Destroy();
        else
            other.GetComponentInParent<ProjectileController>().DestroyProjectile();
    }
}
