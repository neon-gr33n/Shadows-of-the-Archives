using System;
using UnityEngine;

public class NPC : Interactable {
    [SerializeField] private DialogueEvents dialogueEvents;
    [SerializeField] private GameObject textbox;
    [SerializeField] private string[] text;
    [SerializeField] private string charaName;
    [SerializeField] private bool useNameplate = false;
    [SerializeField] private bool usePortrait = false;

    public override void OnInteract()
    {
        base.OnInteract();
        textbox.SetActive(true);
        dialogueEvents.hasNameplate = useNameplate;
        dialogueEvents.hasPortrait = usePortrait;
        AssignText();
    }

    private void AssignText()
    {
        dialogueEvents.characterName = charaName;
        dialogueEvents.sentences = text;
        dialogueEvents.textDisplays[0].text = text[0];

        Debug.Log("Assigned text!!");
    }
}