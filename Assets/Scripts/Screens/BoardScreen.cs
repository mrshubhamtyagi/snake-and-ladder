using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardScreen : MonoBehaviour
{
    [SerializeField] private GameObject board;

    [Header("-----Players-----")]
    public PlayerInfo player1;
    public PlayerInfo player2;
    public PlayerInfo player3;
    public PlayerInfo player4;

    [Header("-----Dice-----")]
    public Image diceImage;
    public int diceNumber = 1;
    public Sprite[] diceNumbers = new Sprite[6];
    private Button diceButton;

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
                player1.gameObject.SetActive(true);
                player1.SetPlayer(null, "Player 1");
                player2.gameObject.SetActive(true);
                player2.SetPlayer(null, "Player 2");
                break;

            case 3:
                player1.gameObject.SetActive(true);
                player1.SetPlayer(null, "Player 1");
                player2.gameObject.SetActive(true);
                player2.SetPlayer(null, "Player 2");
                player3.gameObject.SetActive(true);
                player3.SetPlayer(null, "Player 3");
                break;

            case 4:
                player1.gameObject.SetActive(true);
                player1.SetPlayer(null, "Player 1");
                player2.gameObject.SetActive(true);
                player2.SetPlayer(null, "Player 2");
                player3.gameObject.SetActive(true);
                player3.SetPlayer(null, "Player 3");
                player4.gameObject.SetActive(true);
                player4.SetPlayer(null, "Player 4");
                break;
        }
    }

    public void DeInitializeGame()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        player3.gameObject.SetActive(false);
        player4.gameObject.SetActive(false);

        diceNumber = 0;
        diceImage.sprite = diceNumbers[diceNumber];
    }

    public void Dice_Click()
    {
        StartCoroutine(Co_Dice_Click());
    }

    public IEnumerator Co_Dice_Click()
    {
        diceButton.interactable = false;
        for (float i = 0; i < UIManager.Instance.diceAnimDuration; i += UIManager.Instance.diceAnimDelay)
        {
            yield return new WaitForSeconds(UIManager.Instance.diceAnimDelay);
            diceNumber = Random.Range(0, 6);
            diceImage.sprite = diceNumbers[diceNumber];
        }

        yield return new WaitForSeconds(1);
        diceButton.interactable = true;
    }


    public void Exit_Click(Transform _transform)
    {
        UIManager.isComingFromGameScreen = true;
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.ShowExitPopup();
    }

    public void Sound_Click(Transform _transform)
    {
        UIManager.Instance.ButtonTween(_transform);
        GameManager.Instance.isSoundOn = !GameManager.Instance.isSoundOn;
        soundImage.sprite = GameManager.Instance.isSoundOn ? soundOn : soundOff;
    }

    public void Music_Click(Transform _transform)
    {
        UIManager.Instance.ButtonTween(_transform);
        GameManager.Instance.isMusicOn = !GameManager.Instance.isMusicOn;
        musicImage.sprite = GameManager.Instance.isMusicOn ? musicOn : musicOff;
    }



}
