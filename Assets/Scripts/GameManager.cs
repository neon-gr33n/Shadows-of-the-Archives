using Unity.VisualScripting;
using UnityEngine;

public enum GameState {
    IN_MENU,
    COFNFIG,
    CREDITS,
    IN_LEVEL,
    EVIDENCE,
    LOSE,
    VICTORY
}

public class GameManager : MonoBehaviour {
    public static GameManager Instnace;
    [SerializeField] private GameState gameState;
    public FlagDictionary gameFlags;

    private void Awake() {
        Instnace = this;
    }

    public void SetGameState(GameState state)
    {
        // Change the current game state
        gameState = state;
    }

    public void SetGameFlag(string key, int value)
    {
        gameFlags[key] = value;
    }

    public void ClearGameFlag(string key)
    {
        gameFlags[key] = 0; // Resets a given key to the default value, 0.
    }

}