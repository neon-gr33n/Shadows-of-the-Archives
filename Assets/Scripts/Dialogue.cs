using UnityEngine;
using TMPro;
using System.Collections;
using Rewired;
using Unity.Collections.LowLevel.Unsafe;

public class Dialogue : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI nameDisplay;
    public GameObject namePlate;
    public GameObject cursor;
    public string[] sentences;
    public string[] names;
    private int index = 0;
    private Player player;
    [SerializeField] private float typingSpeed = 0.4f;
    
    // todo: add typing effect, dialogue system, branching dialogue
    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        StartCoroutine(TextUpdate());
    }

    private void Update() {
        if (player.GetButtonDown("Skip") && textDisplay.text == sentences[index]) {
            NextSentence();
        }
    }

    IEnumerator TextUpdate()
    {
        yield return new WaitForSeconds(2f);

        foreach(char c in sentences[index].ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        nameDisplay.text = names[index];
    }

    private void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            nameDisplay.text = "";
            StartCoroutine(TextUpdate());
        } else {
            textDisplay.text = "";
            nameDisplay.text = "";
            namePlate.SetActive(false);
        }
    }
}