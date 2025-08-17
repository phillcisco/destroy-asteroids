using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmaPadrao : Arma
{
    
    [SerializeField] GameObject projectilePrefab;
    Transform projectileLaunchPos;
    void Awake()
    {
        LastFire = Time.time + fireRate;
    }

    void Start()
    {
        projectileLaunchPos = GameObject.FindWithTag("Central").transform;
        _projectileType = projectilePrefab.GetComponent<ProjectileController>().ProjectileTypeID;
    }
   
    public override void Atirar()
    {
        if (Time.time - LastFire > fireRate)
        {
            //Instantiate(projectilePrefab, projectileLaunchPos.position, Quaternion.identity);
            //Buscar da Pool de Projectiles
            GameObject projectile = ProjectilesPool.Instance.RetrieveFromPool(_projectileType);
            projectile.transform.position = projectileLaunchPos.position;
            projectile.SetActive(true);
            LastFire = Time.time;
        }
        
    }
}
