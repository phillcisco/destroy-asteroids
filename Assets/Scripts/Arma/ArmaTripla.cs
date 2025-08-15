using UnityEngine;

public class ArmaTripla : Arma
{
    [SerializeField] GameObject projectilePrefab;
    Transform projectileLaunchPos,projectileLaunchPosLeft,projectileLaunchPosRight;
    
    void Awake()
    {
        lastFire = Time.time + fireRate;
    }

    void Start()
    {
        projectileLaunchPos = GameObject.FindWithTag("Central").transform;
        projectileLaunchPosLeft = GameObject.FindWithTag("LeftPoint").transform;
        projectileLaunchPosRight = GameObject.FindWithTag("RightPoint").transform;
    }

    public override void Atirar()
    {
        if (Time.time - lastFire > fireRate)
        {
            Instantiate(projectilePrefab, projectileLaunchPosLeft.position, Quaternion.identity);
            Instantiate(projectilePrefab, projectileLaunchPos.position, Quaternion.identity);
            Instantiate(projectilePrefab, projectileLaunchPosRight.position, Quaternion.identity);
            lastFire = Time.time;
        }
        
    }
}
