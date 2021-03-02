using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _explosionLight;
    [SerializeField]
    private float _canFire = -1.0f;
    [SerializeField]
    private GameObject _pivotPoint;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _maxAngle=75.0f;
    [SerializeField]
    private LifeManager _lifeManager;
   
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = _pivotPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            ProjectileExplosion();
            FireProjectile();
            
        }
    }   
    void FireProjectile()
    {
        _lifeManager.ShotFired(1);
        float projectileSpeed = 16f;
        _canFire = Time.time + _fireRate;
      
        GameObject projectile= Instantiate(_projectilePrefab);
        projectile.transform.parent = transform.parent;
        projectile.transform.localPosition = new Vector3(0, 1.3f, 0);
        projectile.transform.GetComponent<Rigidbody>().velocity = transform.up * projectileSpeed;
        
        
    }
    void ProjectileExplosion()
    {
        GameObject light = Instantiate(_explosionLight);
        light.transform.parent = transform.parent;
        light.transform.localPosition = new Vector3(0, 1.3f, -0.2f);
        GameObject explosion = Instantiate(_explosionPrefab);
        explosion.transform.parent = transform.parent;
        explosion.transform.localPosition = new Vector3(0, 1.3f, -0.2f);
        StartCoroutine(explosionCountDown(explosion,light));

    }
    IEnumerator explosionCountDown(GameObject explosion,GameObject light)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(light);
        yield return new WaitForSeconds(0.3f);
        Destroy(explosion);
        
    }
    void Rotate()
    {

        transform.parent.rotation = Quaternion.Euler(0f, 0f, 60 * Mathf.Sin(Time.time * _speed));

    }
    public void birdHit()
    {
    }

}
