using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Simply processes user input to move the player
/// </summary>
public class PlayerController : MonoBehaviour
{   
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private UnityEngine.Vector2 movement;
    [SerializeField] private bool isRunning = false;

    private void Awake() {     
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");;
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRunning) {
            isRunning = true;
            movementSpeed *= 2;
        } else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            isRunning = false;
            movementSpeed = 3;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }
}
