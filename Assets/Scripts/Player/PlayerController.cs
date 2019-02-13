using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    [Header("Prefabs")]
    public PlayerCharacter character;
    public TouchJoystick movementStick;
    public TouchJoystick aimStick;
    public CameraFollow camera;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        //character = PlayerCharacter.localInstance;
        //Debug.Log("PlayerCharacter: "+character);
	}
	
	// Update is called once per frame
	void Update () {
        if (character != null)
        {
            move();
            shoot();
        }
    }

    public void setPlayerCharacter(PlayerCharacter c)
    {
        this.character = c;
        camera.toFollow = c.gameObject;
    }

    private void move()
    {
        Vector2 movementDirection = movementStick.DragDirection;
        character.CmdMove(movementDirection);
    }

    private void shoot()
    {
        if (aimStick.currentlyActive) {
            Vector2 shotDirection = aimStick.DragDirection;
            character.CmdTurn(shotDirection);
            character.CmdShoot();
        }
    }

    Vector2 getKeyboardMovement() {
        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        
    }

}
