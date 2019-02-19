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

    // Start is called before the first frame update
    void Start()
    {
        deathtime = Time.time + lifetime;
        //Debug.Log("Deathtime: " + deathtime + " Time: " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        if (deathtime <= Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            PlayerCharacter target = collision.collider.GetComponent<PlayerCharacter>();
            if (target != null)
            {
                target.ChangeHealth(-damage);
            }
        }
        Destroy(gameObject);
    }


}
