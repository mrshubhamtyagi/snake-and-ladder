using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerCount = 2;
    public bool isSoundOn = true;
    public bool isMusicOn = true;

    public Color playerColorRed = Color.red;
    public Color playerColorGreen = Color.green;
    public Color player3ColorBlue = Color.cyan;
    public Color player4ColorYellow = Color.yellow;

    [Header("-----Player Prefs-----")]
    public PlayerPrefsManager playerPrefs;

    [Header("-----Other Refrences-----")]
    public BoardScreen boardScreen;

    public enum Language { English, Tamil };



    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        playerPrefs.ReadPlayerPrefs();
    }

    void Update()
    {

    }
}
