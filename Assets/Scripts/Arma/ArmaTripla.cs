using UnityEngine;

public class ArmaTripla : Arma
{
    [SerializeField] GameObject projectilePrefab;
    int numOfProjectiles = 3;

    Transform[] projectileLaunchPos;
    //Transform projectileLaunchPos,projectileLaunchPosLeft,projectileLaunchPosRight;
    
    void Awake()
    {
        LastFire = Time.time + fireRate;
    }

    void Start()
    {
        projectileLaunchPos = new Transform[numOfProjectiles];
        projectileLaunchPos[0] = GameObject.FindWithTag("Central").transform;
        projectileLaunchPos[1] = GameObject.FindWithTag("LeftPoint").transform;
        projectileLaunchPos[2] = GameObject.FindWithTag("RightPoint").transform;
        _projectileType = projectilePrefab.GetComponent<ProjectileController>().ProjectileTypeID;
    }

    public override void Atirar()
    {
        if (Time.time - LastFire > fireRate)
        {
            for (int i = 0; i < numOfProjectiles; i++)
            {
                GameObject projectile = ProjectilesPool.Instance.RetrieveFromPool(_projectileType);
                projectile.transform.position = projectileLaunchPos[i].position;
                projectile.SetActive(true);
            }

            LastFire = Time.time;
        }
        
    }
}
