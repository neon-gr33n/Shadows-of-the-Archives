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
    public static GameManager Instance;
    [SerializeField] private GameState gameState;
    public FlagDictionary gameFlags;

    private void Awake() {
         if(Instance == null) // If there is no instance already
         {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
         } else if(Instance != this) // If there is already an instance and it's not `this` instance
         {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
         }
        gameFlags.Add("CurrentNight", 0);
        gameFlags.Add("ActionPointsTotal", 10);
        gameFlags.Add("CurrentActionPoints", 10);
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