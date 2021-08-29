using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int> openNumbers = new List<int> { 1, 6 };

    public int playerCount = 2;
    public int playerTurnTimer = 10;
    public int totalPiecesInBoard = 72;
    public bool isSoundOn = true;
    public bool isMusicOn = true;


    [Header("-----Player Settings-----")]
    public float playerMovementSpeed = 1f;
    public DG.Tweening.Ease playerEasyType = DG.Tweening.Ease.Flash;
    public Color playerColorRed = Color.red;
    public Color playerColorGreen = Color.green;
    public Color player3ColorBlue = Color.cyan;
    public Color player4ColorYellow = Color.yellow;

    [Header("-----Player Prefs-----")]
    public PlayerPrefsManager playerPrefs;

    [Header("-----Other Refrences-----")]
    public BoardScreen boardScreen;
    public AudioManager audioManager;


    public enum Language { English, Tamil };
    public enum PlayerIndex { First, Second, Third, Fourth };

    public static Player currentPlayer;
    public static PlayerInfo currentPlayerInfo;



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


    public delegate void OnPlayerTurn(PlayerIndex _index);
    public static event OnPlayerTurn OnPlayerTurnEvent;
    public void Call_OnPlayerTurn(PlayerIndex _index)
    {
        OnPlayerTurnEvent?.Invoke(_index);
    }

}
