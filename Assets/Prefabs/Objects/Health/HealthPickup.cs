using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthPickup : NetworkBehaviour
{
    public int healthRestore = 25;
    public float rotationSpeed = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotationSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;
        PlayerCharacter c = other.GetComponent<PlayerCharacter>();
        if (c)
        {
            c.ChangeHealth(healthRestore);
            Destroy(this.gameObject);
        }
    }
}
