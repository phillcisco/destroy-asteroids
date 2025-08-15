using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmaPadrao : Arma
{
    
    [SerializeField] GameObject projectilePrefab;
    Transform projectileLaunchPos;
    
    void Awake()
    {
        lastFire = Time.time + fireRate;
    }

    void Start()
    {
        projectileLaunchPos = GameObject.FindWithTag("Central").transform;
    }

    public override void Atirar()
    {
        if (Time.time - lastFire > fireRate)
        {
            Instantiate(projectilePrefab, projectileLaunchPos.position, Quaternion.identity);
            lastFire = Time.time;
        }
        
    }
}
