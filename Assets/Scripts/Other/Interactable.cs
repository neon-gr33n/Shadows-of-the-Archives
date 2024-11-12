using Rewired;
using Unity.Mathematics;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    protected bool isFocus = false;
    protected bool hasInteracted = false;
    public Transform player;

    /// <summary>
    /// Method to be overriden upon interacting, such as Items/NPCs
    /// </summary>
    public virtual void OnInteract()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    public virtual void Update() {
        if (!isFocus && !hasInteracted)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= radius){
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
    }

    public void RevokeFocus()
    {
        isFocus = false;
        hasInteracted = false;
    }

}