using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardScreen : MonoBehaviour
{
    public int CUSTOMNUMBER = 2;
    public bool useCustomNumber = false;
    [SerializeField] private GameObject board;

    [Header("-----Players-----")]
    public PlayerInfo playerInfo1;
    public PlayerInfo playerInfo2;
    public PlayerInfo playerInfo3;
    public PlayerInfo playerInfo4;

    [Header("-----Player Info Positions-----")]
    public Vector2 playerInfoPositionFarLeft;
    public Vector2 playerInfoPositionLeft;
    public Vector2 playerInfoPositionRight;
    public Vector2 playerInfoPositionFarRight;


    [Header("-----Dice-----")]
    public Image diceImage;
    public int diceNumber = 1;
    public Sprite[] diceNumbers = new Sprite[6];
    public Button diceButton;
    public bool isDiceRolling = false;

    [Header("-----Sound & Music-----")]
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;



    void Start()
    {
        diceButton = diceImage.GetComponent<Button>();
        //SetBoardSizeBaseOnScreenResolution(Screen.currentResolution);
        DeInitializeGame();
    }

    [ContextMenu("SetBoardSizeBaseOnScreenResolution")]
    public void SetBoardSizeBaseOnScreenResolution(Resolution _res)
    {
        float _aspectRation = _res.height / (float)_res.width;
        print(_aspectRation);
        board.GetComponent<RectTransform>().sizeDelta = _aspectRation > 2 ? new Vector2(1000, 2000) : new Vector2(800, 1600);
    }

    public void InitializeGame()
    {
        switch (GameManager.Instance.playerCount)
        {
            case 2:
                playerInfo1.gameObject.SetActive(true);
                playerInfo1.SetPlayer(null, playerInfoPositionRight, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null, playerInfoPositionFarRight);
                break;

            case 3:
                playerInfo1.gameObject.SetActive(true);
                playerInfo1.SetPlayer(null, playerInfoPositionLeft, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null, playerInfoPositionRight);
                playerInfo3.gameObject.SetActive(true);
                playerInfo3.SetPlayer(null, playerInfoPositionFarRight);
                break;

            case 4:
                playerInfo1.gameObject.SetActive(true);
                playerInfo1.SetPlayer(null, playerInfoPositionFarLeft, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null, playerInfoPositionLeft);
                playerInfo3.gameObject.SetActive(true);
                playerInfo3.SetPlayer(null, playerInfoPositionRight);
                playerInfo4.gameObject.SetActive(true);
                playerInfo4.SetPlayer(null, playerInfoPositionFarRight);
                break;
        }
        diceButton.interactable = true;
    }

    public void DeInitializeGame()
    {
        playerInfo1.gameObject.SetActive(false);
        playerInfo1.gameObject.SetActive(false);
        playerInfo2.gameObject.SetActive(false);
        playerInfo3.gameObject.SetActive(false);
        playerInfo4.gameObject.SetActive(false);

        isDiceRolling = false;
        diceNumber = 0;
        diceImage.sprite = diceNumbers[diceNumber];
        GameManager.currentPlayer = null;
        GameManager.currentPlayerInfo = null;
        diceButton.interactable = false;
    }

    public void Dice_Click()
    {
        StartCoroutine(Co_Dice_Click());
    }

    public IEnumerator Co_Dice_Click()
    {
        isDiceRolling = true;
        GameManager.Instance.audioManager.PlayDiceSound();
        for (float i = 0; i < UIManager.Instance.diceAnimDuration; i += UIManager.Instance.diceAnimDelay)
        {
            yield return new WaitForSeconds(UIManager.Instance.diceAnimDelay);
            diceNumber = Random.Range(1, 7);
            diceImage.sprite = diceNumbers[diceNumber - 1];
        }
        diceButton.interactable = false;
        if (useCustomNumber)
        {
            diceNumber = CUSTOMNUMBER;
            diceImage.sprite = diceNumbers[CUSTOMNUMBER - 1];
        }

        print("Dice Number = " + diceNumber);
        yield return new WaitForSeconds(0.25f);

        if (!GameManager.currentPlayer.hasPlayerStarted && !GameManager.Instance.openNumbers.Contains(diceNumber))
        {
            diceButton.interactable = true;
            GameManager.currentPlayerInfo.OnPlayerTurnOver();
            isDiceRolling = false;
            yield break;
        }


        if (diceNumber > (GameManager.Instance.totalPiecesInBoard - GameManager.currentPlayer.currentPieceNumber))
        {
            print("Invalid Turn");
            diceButton.interactable = true;
            GameManager.currentPlayerInfo.OnPlayerTurnOver();
            isDiceRolling = false;
            yield break;
        }

        isDiceRolling = false;
        print(GameManager.currentPlayer.playerInfo.playerDisplayName);

        if (!GameManager.currentPlayer.hasPlayerStarted && diceNumber == 6)
        {
            print("6 case ------" + diceImage);
            diceNumber = 1;
        }

        GameManager.currentPlayer.hasPlayerStarted = true;
        GameManager.currentPlayer.MovePlayerBySteps(diceNumber);
    }


    public void Exit_Click(Transform _transform)
    {
        GameManager.Instance.audioManager.PlayBubbleSound();
        UIManager.isComingFromGameScreen = true;
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.ShowExitPopup();
    }

    public void Sound_Click(Transform _transform)
    {
        GameManager.Instance.audioManager.PlayBubbleSound();
        GameManager.Instance.audioManager.ToggleSound();
        UIManager.Instance.ButtonTween(_transform);
        GameManager.Instance.isSoundOn = !GameManager.Instance.isSoundOn;
        soundImage.sprite = GameManager.Instance.isSoundOn ? soundOn : soundOff;
    }

    public void Music_Click(Transform _transform)
    {
        GameManager.Instance.audioManager.PlayBubbleSound();
        GameManager.Instance.audioManager.ToggleMusic();
        UIManager.Instance.ButtonTween(_transform);
        GameManager.Instance.isMusicOn = !GameManager.Instance.isMusicOn;
        musicImage.sprite = GameManager.Instance.isMusicOn ? musicOn : musicOff;
    }



}
