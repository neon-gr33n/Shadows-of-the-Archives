using UnityEditor;
using UnityEngine;

public class InteractComponent : MonoBehaviour {
    public Interactable focus;
    [SerializeField] private NPC npc;
    private Camera cam;

    // Used for initialization
    private void Start() {
        cam = Camera.main;
    }

    private void Interact(){
        if (focus != null)
        {
            string focusTag = focus.tag;
            switch(focusTag)
            {
                case "NPC":
                    focus.OnInteract();
                break;
                case "Item":
                    focus.OnInteract();
                break;
                case "Object":
                    focus.OnInteract();
                break;                
            }

        }
    }

    /// <summary>
    /// Swap focus from one interactable to a new one
    /// </summary>
    /// <param name="newFocus"></param>
    private void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    // Remove the focus
    public void RemoveFocus()
    {
        if (focus == null) return;

        focus.RevokeFocus();
        focus = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter trigger 2D");
        switch(other.gameObject.tag)
        {
            case "NPC":
               npc = other.gameObject.GetComponent<NPC>();
               SetFocus(npc);
               if(focus != null)
               {
                 npc.OnInteract();
               }
            break;
            // case "Item":
            // break;
            // case "Object":
            // break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited Trigger 2D");
        RemoveFocus(); 
    }

}