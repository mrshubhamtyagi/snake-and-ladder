using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerParent;
    public Vector2 startPosition;
    public string playerDisplayName;
    public Image timerImage;
    public Image borderImage;


    [Header("-----Other References-----")]
    public GameManager.PlayerIndex playerIndex;
    public Color playerColor;
    public Image playerImage;
    public TextMeshProUGUI playerName;
    public float timer;
    private float inSeconds;
    public bool isActivePlayer = false;



    private Player playerScript;
    private float downScale = 0.75f;




    void OnEnable()
    {
        GameManager.OnPlayerTurnEvent += Event_OnPlayerTurn;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerTurnEvent -= Event_OnPlayerTurn;
    }

    public void SetPlayer(Sprite _image, bool _isActivePlayer = false)
    {
        //playerImage.sprite = _image;
        print("SetPlayer_" + playerDisplayName);
        playerName.text = playerDisplayName;
        playerScript = playerParent.childCount >= (int)playerIndex + 1 ? playerParent.GetChild((int)playerIndex).GetComponent<Player>() : Instantiate(playerPrefab, playerParent).GetComponent<Player>();
        playerScript.gameObject.name = playerDisplayName;
        playerScript.transform.GetChild(0).GetComponent<Image>().color = playerColor;
        playerScript.playerInfo = this;
        playerScript.transform.localPosition = startPosition;
        playerScript.currentPieceNumber = 0;
        playerScript.hasPlayerStarted = playerScript.isPlayerMoving = false;

        timerImage.fillAmount = timer = inSeconds = 0;
        isActivePlayer = _isActivePlayer;
        borderImage.transform.localScale = isActivePlayer ? Vector3.one : Vector3.one * downScale;
        if (isActivePlayer)
        {
            GameManager.currentPlayer = playerScript;
            GameManager.currentPlayerInfo = this;
        }
    }

    void Update()
    {
        if (!isActivePlayer || playerScript.isPlayerMoving || GameManager.Instance.boardScreen.isDiceRolling)
            return;

        if (inSeconds > GameManager.Instance.playerTurnTimer)
        {
            OnPlayerTurnOver();
        }
        else
        {
            timer += Time.deltaTime;
            inSeconds = timer % 60;
            timerImage.fillAmount = inSeconds / GameManager.Instance.playerTurnTimer;
        }
    }

    public void Event_OnPlayerTurn(GameManager.PlayerIndex _index)
    {
        //print(playerIndex + "<-CURRENT OnPlayerTurn NEXT-> " + _index);
        if (playerIndex == _index)
        {
            isActivePlayer = true;
            borderImage.transform.localScale = Vector3.one;
            GameManager.currentPlayer = playerScript;
            GameManager.currentPlayerInfo = this;
        }
        else
        {
            borderImage.transform.localScale = Vector3.one * downScale;
        }
    }

    public void OnPlayerTurnOver()
    {
        timerImage.fillAmount = timer = inSeconds = 0;
        isActivePlayer = false;
        borderImage.transform.localScale = Vector3.one;
        //borderImage.color = Color.white;


        // Set Next Player Turn
        int _nextPlayerIndex = ((int)playerIndex + 1) % GameManager.Instance.playerCount;
        print("NEXT PLAYER -> " + (_nextPlayerIndex + 1));
        GameManager.Instance.Call_OnPlayerTurn((GameManager.PlayerIndex)_nextPlayerIndex);
        GameManager.Instance.boardScreen.diceButton.interactable = true;
    }


}