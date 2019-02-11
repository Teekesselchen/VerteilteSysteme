using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerCharacter character;
    public TouchJoystick movementStick;
    public TouchJoystick aimStick;
    public WeaponContainer weapon;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        move();
        shoot();
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
            character.turn(shotDirection);
            weapon.shoot();
        }
    }

    Vector2 getKeyboardMovement() {
        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        
    }

}
