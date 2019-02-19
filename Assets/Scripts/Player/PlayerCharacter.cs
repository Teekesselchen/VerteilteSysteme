﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : NetworkBehaviour
{
    CharacterController cc;
    Animator anim;

    [Header("Stats")]
    public float speed = 5;
    public int health = 100;
    private int startingHealth;

    [Header("Components")]
    public Weapon weapon;
    public GameObject controllerPrefab;
    public GameObject serverCameraPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        this.anim = GetComponent<Animator>();
        startingHealth = health;
        if (isLocalPlayer)
        {
            Debug.Log("Client: "+isClient);
            Debug.Log("Server: " + isServer);
            if (isServer)
            {
                GameObject.Instantiate(serverCameraPrefab);
                Destroy(gameObject);
            } else
            {
                GameObject controller = GameObject.Instantiate(controllerPrefab);
                PlayerController pc = controller.GetComponent<PlayerController>();
                if (pc)
                {
                    pc.setPlayerCharacter(this);
                }
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(Vector2 direction)
    {
        if (!anim)
            return;
        if (direction.magnitude == 0)
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("Direction", 0);
        } else
        {
            Vector3 worldDirection = new Vector3(
                direction.x,
                0,
                direction.y
            ).normalized;

            // limit speed to {0, 1}
            float currentSpeed = speed * (direction.magnitude > 1 ? 1 : direction.magnitude);
            // cross product points up: character moves to left. Points down: move to right
            float sidefactor = Vector3.Cross(worldDirection, transform.forward).y > 0 ? -0.5f : 0.5f;
            // 0 = forward, 1 = sideways, 2 = backwards
            float angle = (1 - Vector3.Dot(worldDirection, transform.forward)) * sidefactor;
            anim.SetFloat("Speed", currentSpeed);
            anim.SetFloat("Direction", angle);
            CmdMove(worldDirection * currentSpeed * Time.deltaTime);
        }
    }

    [Command]
    void CmdMove(Vector3 mv)
    {
        cc.Move(mv);
    }

    [Command]
    public void CmdTurn(Vector2 direction) {
        Vector3 worldDirection = new Vector3(
                direction.x,
                0,
                direction.y
        );
        transform.LookAt(transform.position + worldDirection);
    }

    [Command]
    public void CmdShoot()
    {
        weapon.shoot();
    }

    [ClientRpc]
    void RpcHealth(int change) {
        health += change;
    }

    public void ChangeHealth(int change) {
        if (isServer) {
            health += change;
            RpcHealth(change);
        }
    }

    public float getRelativeHealth()
    {
        return health/startingHealth;
    }
}
