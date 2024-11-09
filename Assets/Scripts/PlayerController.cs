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
    public int WalkSpeed = 2;
    public int RunSpeed = 4;
    private Rigidbody2D rb;   
    private Player player; // The Rewired Player
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private UnityEngine.Vector2 movement;
    [SerializeField] private bool isRunning = false;

    private void Awake() {     
        rb = GetComponent<Rigidbody2D>();
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        movement.x = player.GetAxisRaw("Move Horizontal");
        movement.y = player.GetAxisRaw("Move Vertical");
        movement = movement.normalized;

        if (player.GetButtonDown("Run") && !isRunning) {
            isRunning = true;
            movementSpeed = RunSpeed;
        } else if(player.GetButtonDown("Run")) {
            isRunning = false;
            movementSpeed = WalkSpeed;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }
}
