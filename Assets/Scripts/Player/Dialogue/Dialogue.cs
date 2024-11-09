using UnityEngine;
using TMPro;
using System.Collections;
using Rewired;

public class Dialogue : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index = 0;
    private Player player;
    [SerializeField] private float typingSpeed = 0.4f;
    
    // todo: add typing effect, dialogue system, branching dialogue
    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        StartCoroutine(Type());
    }

    private void Update() {
        if (player.GetButtonDown("Skip") && textDisplay.text == sentences[index]) {
            NextSentence();
        }
    }

    IEnumerator Type()
    {
        yield return new WaitForSeconds(2f);

        foreach(char c in sentences[index].ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";

        }
    }
}