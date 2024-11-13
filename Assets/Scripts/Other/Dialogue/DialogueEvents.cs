using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using TMPro;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class DialogueEvents : MonoBehaviour {
    public GameObject[] textDisplays;
    public GameObject[] Choices;
    public GameObject portrait;
    public UnityEngine.UI.Image portraitImage;
    public Transform dialogueOffsetWithPortrait;
    public Sprite[] portraits;

    public DialogueNode[] dialogueNodes;  // Array of DialogueNodes (each node can have actions, choices, and text)
    private int currentNodeIndex = 0;
    private Player player;
    private InputComponent inputs;
    private PlayerController playerController;

    public bool hasPortrait = false;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<InputComponent>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        if (inputs.Skip && !HasChoices()) {
            NextSentence();
        }
    }

    public void ShowDialogueNode(DialogueNode node) {
        // Display character name

        Debug.Log(node.characterName);
        Debug.Log(node.text);

        // Display text
        textDisplays[1].GetComponent<TextMeshProUGUI>().text = node.characterName;
        textDisplays[0].GetComponent<TextMeshProUGUI>().text = node.text;

        // Trigger actions for this node
        TriggerActions(node.actions);

        // If the node has choices, show them
        if (node.choices.Count > 0)
        {
            Choices[0].transform.parent.gameObject.SetActive(true);
            DisplayChoices(node.choices);
        }
        else
        {
            // Hide choices if there are none
            foreach (var choice in Choices)
            {
                choice.GetComponent<TextMeshProUGUI>().text = "";
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
        // for (int i = 0; i < choices.Count; i++) {
        //     // For simplicity, just update the first 3 text displays with choices
        //     Choices[i]
        // }
        for (int i=0; i < choices.Count; i++)
        {
            Choices[i].SetActive(true);
            Choices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choices[i].text;
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
            textDisplays[i].GetComponent<TextMeshProUGUI>().text = "";
        }

        playerController.UnFreezePlayer();

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

    public void Test1()
    {
        Debug.Log("1");
    }
    public void Test2()
    {
        Debug.Log("2");
    }
    public void Test3()
    {
        Debug.Log("3");
    }
}
