using UnityEngine;
using TMPro;
using System.Collections;
using Rewired;
using TMPEffects;
using TMPEffects.Components;
using UnityEngine.UI;

public class DialogueEvents : MonoBehaviour {
    public TextMeshProUGUI[] textDisplays;
    public GameObject portrait;
    public Image portraitImage;
    public GameObject nameplate;
    public Transform dialogueOffsetWithPorait;
    public Sprite[] portraits;

    public string[] sentences;
    public string characterName;
    private int index = 0;
    private Player player;
    private InputComponent inputs;
    public bool hasPortrait = false;
    public bool hasNameplate = false;
    
    // todo: add branching dialogue, ability to execute events like camera movement during or after lines of dialogue
    // allow a delay between aforementioned events and the next line of dialogue, allow choices
    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<InputComponent>();
        textDisplays[0].text = sentences[0];

        if (hasPortrait == true){
            portrait.SetActive(true);
            textDisplays[0].transform.position = dialogueOffsetWithPorait.transform.position;
        } else {
            portrait.SetActive(false);
        }

        if (hasNameplate == true){
            nameplate.SetActive(true);
        } else {
            nameplate.SetActive(false);
        }
    }

    private void Update() {
        if (inputs.Skip && textDisplays[0].text == sentences[index]) {
            NextSentence();
        }
    }

    private void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            if(hasPortrait == true){
                portraitImage.sprite = portraits[index];
            }
            for(int i = 0; i < textDisplays.Length - 1; i++){
                textDisplays[i].text = "";
            }

            textDisplays[0].text = sentences[index].ToString();
            textDisplays[1].text = characterName;
        } else {
            // Clears the current text before we disable the object.
            for(int i = 0; i < textDisplays.Length - 1; i++){
                textDisplays[i].text = "";
            }

            for(int j = 0; j < sentences.Length - 1; j++)
            {
                sentences[j] = "";
            }

            gameObject.SetActive(false);
        }
    }

}