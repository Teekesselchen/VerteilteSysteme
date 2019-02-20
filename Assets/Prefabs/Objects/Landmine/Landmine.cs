using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Landmine : NetworkBehaviour
{
    public float beepPeriod = 2.0f;
    public float armTime = 1.0f;
    public float damage = 100;
    public float damageRadius = 2.0f;
    public float armingRadius = 1.0f;
    public Light l;
    public GameObject explosion;

    List<PlayerCharacter> nearbyCharacters = new List<PlayerCharacter>();
    float ttl = 0;
    float lightIntensityInitial;
    float lightIntensityCurrent;
    float currentBeepPeriod;
    bool armed = false;

    // Start is called before the first frame update
    void Start()
    {
        lightIntensityInitial = l.intensity;
        currentBeepPeriod = beepPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            checkActivation();
        }
        setLightIntensity();
    }

    private void setLightIntensity()
    {
        if(armed)
        {
            if(ttl > 0)
            {
                currentBeepPeriod = beepPeriod * ttl;
                ttl -= Time.deltaTime;
            } else
            {
                GameObject e = GameObject.Instantiate<GameObject>(explosion, transform.position, transform.rotation, null);
                Destroy(gameObject);
                Destroy(e, 1.0f);
            }
            
        }
        l.intensity = lightIntensityInitial * Mathf.Abs(Mathf.Cos(Mathf.PI * Time.time / currentBeepPeriod));
    }

    private void checkActivation()
    {
        if (!armed)
        {
            foreach (PlayerCharacter c in nearbyCharacters)
            {
                float dist = Vector3.Distance(transform.position, c.transform.position);
                if (dist <= armingRadius)
                {
                    armed = true;
                    ttl = armTime;
                    RpcActivate();
                }
            }
        } else if (ttl <= 0)
        {
            foreach (PlayerCharacter c in nearbyCharacters)
            {
                float dist = Vector3.Distance(transform.position, c.transform.position);
                if (dist <= damageRadius)
                {
                    float dmg = this.damage * (dist / damageRadius);
                    c.ChangeHealth(-(int)dmg);
                }
            }
        }
    }

    [ClientRpc]
    public void RpcActivate()
    {
        armed = true;
        ttl = armTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            PlayerCharacter c = other.GetComponent<PlayerCharacter>();
            if (c != null && !nearbyCharacters.Contains(c))
            {
                nearbyCharacters.Add(c);
            }
        }
    }
        
}
