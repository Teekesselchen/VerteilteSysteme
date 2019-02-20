using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform bulletSpawn;
    public float reload = 0.1f;
    private float nextReloadTime = 0.0f;
    private int playerId = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerId(int i)
    {
        playerId = i;
    }

    public GameObject shoot()
    {
        if (nextReloadTime <= Time.time) {
            nextReloadTime = Time.time + reload;
            GameObject bullet = GameObject.Instantiate<GameObject>(projectile, bulletSpawn.position, bulletSpawn.rotation);
            Projectile p = bullet.GetComponent<Projectile>();
            p.initProjectile(playerId);
            return bullet;
        }
        return null;
    }
}
