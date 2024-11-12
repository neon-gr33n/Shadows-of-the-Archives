using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using TMPro;
using System.Text.RegularExpressions;

public class DialogueEvents : MonoBehaviour {
    public TextMeshProUGUI[] textDisplays;
    public GameObject portrait;
    public Image portraitImage;
    public Transform dialogueOffsetWithPortrait;
    public Sprite[] portraits;

    public DialogueNode[] dialogueNodes;  // Array of DialogueNodes (each node can have actions, choices, and text)
    private int currentNodeIndex = 0;
    private Player player;
    private InputComponent inputs;

    public bool hasPortrait = false;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<InputComponent>();

        // Start by showing the first node
        // todo: Fix an issue with this function where nodes assigned from an NPC
        // do not display in the dialogue box
        ShowDialogueNode(dialogueNodes[currentNodeIndex]);
    }

    private void Update() {
        if (inputs.Skip && !HasChoices()) {
            NextSentence();
        }
    }

    public void ShowDialogueNode(DialogueNode node) {
        // Display character name
        textDisplays[1].text = node.characterName;

        // Display text
        textDisplays[0].text = node.text;

        // Trigger actions for this node
        TriggerActions(node.actions);

        // If the node has choices, show them
        if (node.choices.Count > 0) {
            DisplayChoices(node.choices);
        } else {
            // Hide choices if there are none
            foreach (var choice in textDisplays) {
                choice.text = "";
            }
        }

        // Handle portrait display
        if (hasPortrait && portraits.Length > currentNodeIndex) {
            portraitImage.sprite = portraits[currentNodeIndex];
            portrait.SetActive(true);
        } else {
            portrait.SetActive(false);
        }
    }

    private bool HasChoices() {
        return dialogueNodes[currentNodeIndex].choices.Count > 0;
    }

    private void DisplayChoices(List<Choice> choices) {
        // Assuming you have buttons to display choices, here we just print them for now
        for (int i = 0; i < choices.Count; i++) {
            // For simplicity, just update the first 3 text displays with choices
            textDisplays[i].text = choices[i].text;
        }
    }

    private void NextSentence() {
        currentNodeIndex++;

        if (currentNodeIndex < dialogueNodes.Length) {
            ShowDialogueNode(dialogueNodes[currentNodeIndex]);
        } else {
            EndDialogue();
        }
    }

    private void EndDialogue() {
        for (int i = 0; i < textDisplays.Length; i++) {
            textDisplays[i].text = "";
        }

        gameObject.SetActive(false);
    }

    private void TriggerActions(List<Action> actions) {
        foreach (Action action in actions) {
            ExecuteAction(action);
        }
    }

    private void ExecuteAction(Action action) {
        switch (action.type) {
            case "move_camera":
                MoveCamera(action.parameters);
                break;
            case "set_flag":
                SetFlag(action.parameters);
                break;
            // Add more cases as needed
            default:
                Debug.LogWarning("Unknown action type: " + action.type);
                break;
        }
    }

    private void MoveCamera(Dictionary<string, string> parameters) {
        float x = float.Parse(parameters["x"]);
        float y = float.Parse(parameters["y"]);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
    }

    private void SetFlag(Dictionary<string, string> parameters) {
        string key = parameters["key"];
        int value = int.Parse(parameters["value"]);
        
        GameManager.Instance.SetGameFlag(key, value);
    }
}
