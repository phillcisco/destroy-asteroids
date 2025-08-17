using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    protected float LastFire;
    protected ProjectileType _projectileType;
    
    public abstract void Atirar();

}