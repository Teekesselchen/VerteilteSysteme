﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : NetworkBehaviour
{
    //public static PlayerCharacter localInstance;
    CharacterController cc;
    Animator anim;

    [Header("Stats")]
    public float speed = 5;
    public int health = 100;
    // Start is called before the first frame update

    void Start()
    {
        cc = GetComponent<CharacterController>();
        this.anim = GetComponent<Animator>();
        if (isLocalPlayer)
        {
            //localInstance = this;
            PlayerController c = PlayerController.instance;
            if(c != null)
            {
                c.setPlayerCharacter(this);
                Debug.Log("Local Instance Set in Start.");
            }
            
        }
        else
        {
            Debug.Log("I am not local player in Start?");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //[Command]
    public void CmdMove(Vector2 direction)
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
            cc.Move(worldDirection * currentSpeed * Time.deltaTime);
            //Debug.Log("Direction: " + angle);
            //Debug.Log("Speed: " + currentSpeed);
            //Debug.Log("Side: " + sidefactor);
        }
        
    }

    //[Command]
    public void CmdTurn(Vector2 direction) {
        Vector3 worldDirection = new Vector3(
                direction.x,
                0,
                direction.y
        );
        transform.LookAt(transform.position + worldDirection);
    }

    [ClientRpc]
    void RpcHealth(int change) {
        Debug.Log("Change Health: " + change);
    }

    public void ChangeHealth(int change) {
        if (!isServer) {
            return;
        }

        health += change;
        RpcHealth(change);
    }
}
