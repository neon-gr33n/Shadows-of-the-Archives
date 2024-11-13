using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Rewired;

/// <summary>
/// Simply processes user input to move the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float WalkSpeed = 2f;
    [SerializeField] private float RunSpeed = 4f;
    private Rigidbody2D rb;   
    private InputComponent inputs;
    private float movementSpeed = 4f;
    [SerializeField] private UnityEngine.Vector2 movement;

    public bool moveable = true;

    private void Awake() {     
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<InputComponent>();
    }
    
    void Update()
    {
        movement.x = inputs.Horizontal;
        movement.y = inputs.Vertical;
        movement = movement.normalized;
        movementSpeed = inputs.Run ? RunSpeed : WalkSpeed;
    }

    private void FixedUpdate() {
        if (moveable == true) {
          rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
        }
    }

    public void FreezePlayer()
    {
        moveable = false;
    }

    public void UnFreezePlayer()
    {
        moveable = true;
    }
}
