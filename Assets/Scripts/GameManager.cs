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

    private void Awake() {
        Instnace = this;
    }

    public void SetGameState(GameState state)
    {
        // Change the current game state
        gameState = state;
    }
}