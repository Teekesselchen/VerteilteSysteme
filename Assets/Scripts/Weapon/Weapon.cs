using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform bulletSpawn;
    public float reload = 0.1f;
    private float nextReloadTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        if (nextReloadTime <= Time.time) {
            nextReloadTime = Time.time + reload;
            GameObject.Instantiate<GameObject>(projectile, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
}
