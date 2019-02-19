using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //public static PlayerController instance;
    [Header("Prefabs")]
    public PlayerCharacter character;
    public TouchJoystick movementStick;
    public TouchJoystick aimStick;
    public CameraFollow camera;
    public Slider healthbar;

    void Awake()
    {
        //instance = this;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (character != null)
        {
            move();
            shoot();
            setHealthbar();
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
        character.move(movementDirection);
    }

    private void shoot()
    {
        if (aimStick.currentlyActive) {
            Vector2 shotDirection = aimStick.DragDirection;
            character.CmdTurn(shotDirection);
            character.CmdShoot();
        }
    }

    private void setHealthbar()
    {
        healthbar.normalizedValue = character.getRelativeHealth();
    }

    Vector2 getKeyboardMovement() {
        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        
    }

}
