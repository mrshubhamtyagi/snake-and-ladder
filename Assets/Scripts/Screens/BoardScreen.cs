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
                playerInfo1.SetPlayer(null, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null);
                break;

            case 3:
                playerInfo1.gameObject.SetActive(true);
                playerInfo1.SetPlayer(null, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null);
                playerInfo3.gameObject.SetActive(true);
                playerInfo3.SetPlayer(null);
                break;

            case 4:
                playerInfo1.gameObject.SetActive(true);
                playerInfo1.SetPlayer(null, true);
                playerInfo2.gameObject.SetActive(true);
                playerInfo2.SetPlayer(null);
                playerInfo3.gameObject.SetActive(true);
                playerInfo3.SetPlayer(null);
                playerInfo4.gameObject.SetActive(true);
                playerInfo4.SetPlayer(null);
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
        GameManager.currentPlayer.hasPlayerStarted = true;
        print(GameManager.currentPlayer.playerInfo.playerDisplayName);
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
