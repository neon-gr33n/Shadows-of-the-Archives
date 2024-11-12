using System;
using UnityEngine;

public class NPC : Interactable {
    [SerializeField] private DialogueEvents dialogueEvents;
    [SerializeField] private GameObject textbox;
   // [SerializeField] private string[] text;  // Old format (just text lines)
    [SerializeField] private string charaName;
    [SerializeField] private bool usePortrait = false;

    // Add this to allow for dialogue nodes in the Inspector
    [SerializeField] private DialogueNode[] dialogueNodes;  // Array of DialogueNodes

    public override void OnInteract()
    {
        base.OnInteract();
        
        // Show the textbox and set up dialogue
        textbox.SetActive(true);

        // Set portrait usage (if we have a portrait or not)
        dialogueEvents.hasPortrait = usePortrait;
        
        // Assign dialogue data to the DialogueEvents system
        AssignDialogue();
    }

    private void AssignDialogue()
    {
        // Assign the character name
        dialogueEvents.textDisplays[1].text = charaName;

        // Now we assign the DialogueNodes instead of just a string array
        dialogueEvents.dialogueNodes = dialogueNodes;

        // Optionally, you can set the first dialogue node explicitly to start the dialogue
        dialogueEvents.ShowDialogueNode(dialogueEvents.dialogueNodes[0]);
    }

    // If you want to assign the text the old way (before using DialogueNodes), you can keep this
    // private void AssignText()
    // {
    //     dialogueEvents.characterName = charaName;
    //     dialogueEvents.sentences = text;
    //     dialogueEvents.textDisplays[0].text = text[0];
    // 
    //     Debug.Log("Assigned text!!");
    // }
}
