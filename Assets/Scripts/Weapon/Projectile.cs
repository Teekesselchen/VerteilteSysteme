using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public int damage = 10;
    private float deathtime;
    private int shooterId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*if (isServer)
      {
          if (deathtime <= Time.time)
          {
              NetworkServer.Destroy(gameObject);
          }
      }*/
    }

    private void FixedUpdate()
    {
        if (isServer)
        {
            transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
            if (deathtime <= Time.time)
            {
                NetworkServer.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Bullet Collision with " + other.gameObject + " on server? " + isServer);
        if (isServer)
        {
            PlayerCharacter target = other.gameObject.GetComponent<PlayerCharacter>();
            if (target != null)
            {
                if (target.getPlayerId() != shooterId)
                {
                    target.ChangeHealth(-damage, shooterId);
                    Debug.Log("Player " + target.getPlayerId() + " hit by " + shooterId);
                    NetworkServer.Destroy(gameObject);
                }
                
            } else
            {
                NetworkServer.Destroy(gameObject);
            }
        }
    }


    public void initProjectile(int shooterId)
    {
        this.shooterId = shooterId;
        deathtime = Time.time + lifetime;
    }
}
