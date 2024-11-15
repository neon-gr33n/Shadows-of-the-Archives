using System.Collections.Generic;

[System.Serializable]
public class DialogueNode {
    public string text;
    public List<Action> actions;
    public List<Choice> choices;
    public string characterName;

    public DialogueNode(string text, string characterName) {
        this.text = text;
        this.characterName = characterName;
        actions = new List<Action>();
        choices = new List<Choice>();
    }

    // Adds a new action to the node
    public void AddAction(Action action) {
        actions.Add(action);
    }

    // Adds a choice for branching dialogue
    public void AddChoice(Choice choice) {
        choices.Add(choice);
    }
}

[System.Serializable]
public class Action {
    public string type;
    public Dictionary<string, string> parameters;

    public Action(string type) {
        this.type = type;
        parameters = new Dictionary<string, string>();
    }

    public void AddParameter(string key, string value) {
        parameters[key] = value;
    }
}

[System.Serializable]
public class Choice {
    public string text;
    public DialogueNode[] nextNode;
    public List<Action> actions;

    public Choice(string text, DialogueNode[] nextNode) {
        this.text = text;
        this.nextNode = nextNode;
        actions = new List<Action>();
    }

    public void AddAction(Action action) {
        actions.Add(action);
    }
}