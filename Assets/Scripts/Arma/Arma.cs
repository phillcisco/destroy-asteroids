using UnityEngine;

public abstract class Arma : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    protected float lastFire;
    
    public abstract void Atirar();

}