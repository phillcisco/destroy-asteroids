using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] Transform projectileSpawnPos;
    [SerializeField] float speed;
    [SerializeField] float intensityRate;
    [field: SerializeField] public ProjectileType ProjectileTypeID { get; set; }
    Material _mat;
    Rigidbody2D _rb;
    
    float _intensity = 1;
    int _intensityDir;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    void OnEnable()
    {
        _rb.AddForceX(speed, ForceMode2D.Impulse);
    }
   
    void Update()
    {
        BlinkProjectileVFX();
    }

    void BlinkProjectileVFX()
    {
        _intensity += intensityRate * Time.deltaTime * _intensityDir;
        _mat.color = new Color(1*_intensity,1*_intensity,1*_intensity);
        if (_intensity >= 3) _intensityDir = -1;
        if (_intensity <= 1) _intensityDir = 1;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            ProjectileVFXPool.Instance.PlayVFX(projectileSpawnPos);
        }
        ProjectilesPool.Instance.ReturnToPool(ProjectileTypeID, gameObject);
    }
}

public enum ProjectileType
{
    Standard, Heavy
}