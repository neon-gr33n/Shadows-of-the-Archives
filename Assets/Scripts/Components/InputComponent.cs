using System;
using UnityEngine;
using Rewired;

public class InputComponent : MonoBehaviour {
    public float Horizontal {get; set;}
    public float Vertical {get; set;}
    public bool Run {get; set;}
    public bool Skip {get; set;}
    public bool Interact {get; set;}
    private Player player;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update() {
        Horizontal = player.GetAxisRaw("Move Horizontal");
        Vertical = player.GetAxisRaw("Move Vertical");
        Run = player.GetButton("Run");
        Interact = player.GetButtonDown("Perform Action");
        Skip = player.GetButtonDown("Skip");

        /*if (Interact){
            Debug.Log("Interacting!");
        }*/

    }

}