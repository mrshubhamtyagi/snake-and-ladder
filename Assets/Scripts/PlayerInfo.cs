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
    public Image timer;
    public Image borderImage;


    [Header("-----Other References-----")]
    public PlayerIndex playerIndex;
    public Color playerColor;
    public Image playerImage;
    public TextMeshProUGUI playerName;


    public enum PlayerIndex { First, Second, Third, Fourth };

    void Start()
    {
        
    }

    public void SetPlayer(Sprite _image, string _name)
    {
        //playerImage.sprite = _image;
        playerName.text = _name;
        Player _player = Instantiate(playerPrefab, playerParent).GetComponent<Player>();
        _player.gameObject.name = _name;
        _player.GetComponent<Image>().color = playerColor;
        _player.transform.localPosition = BoardManager.Instance.piecesPositions[0];
        _player.playerInfo = this;
    }

    void Update()
    {

    }

    public void OnPlayerTurn()
    {
        borderImage.color = Color.black;
    }

    public void OnPlayerTurnOver()
    {
        timer.fillAmount = 0;
        borderImage.color = Color.white;
    }

}
