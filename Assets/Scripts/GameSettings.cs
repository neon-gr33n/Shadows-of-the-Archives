using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameWindowMode 
{
    Windowed,
    Fullscreen,
    Borderless
}
public class GameSettings : MonoBehaviour
{
    public bool isFullscreen = false;
    public bool vsyncEnabled = false;
    public GameWindowMode windowMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
