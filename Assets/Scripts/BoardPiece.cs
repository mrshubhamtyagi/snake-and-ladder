using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public int pieceNumber;
    public bool hasJump = false;
    public int jumpToNumber;
    public bool isFirstPiece = false;
    public bool isLastPiece = false;

    public bool hasPicture = false;
    public int avatarIndex;

    public bool hasPlayer = false;
    [SerializeField] private int totalPlayersOnThisPiece = 0;


    List<Vector2> playerPositions = new List<Vector2>();

    void Start()
    {
        SetPieceInfo();
        SetupAlternatePositions();
    }

    private void SetPieceInfo()
    {
        pieceNumber = transform.GetSiblingIndex() + 1;
    }

    private void SetupAlternatePositions()
    {
        Vector2 centerPosition = BoardManager.Instance.piecesPositions[pieceNumber - 1];
        Vector2 topLeftCorner = new Vector2(centerPosition.x - 30, centerPosition.y + 30);
        Vector2 topRightCorner = new Vector2(centerPosition.x + 30, centerPosition.y + 30);
        Vector2 bottmRightCorner = new Vector2(centerPosition.x + 30, centerPosition.y - 30);
        Vector2 bottomLeftCorner = new Vector2(centerPosition.x - 30, centerPosition.y - 30);

        playerPositions.Add(centerPosition);
        playerPositions.Add(topRightCorner);
        playerPositions.Add(bottmRightCorner);
        playerPositions.Add(bottomLeftCorner);

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            hasPlayer = true;
            totalPlayersOnThisPiece++;

            Player _player = collision.gameObject.GetComponent<Player>();
            _player.currentPieceNumber = pieceNumber;
            if (isLastPiece)
            {
                print("Game Over -> " + _player.name + " wins");
                GameManager.Instance.audioManager.PlayCongratsSound();
                UIManager.Instance.popupManager.ShowCongratsPopup();
                return;
            }

            if (hasPicture || hasJump)
                StartCoroutine(CheckForSpecialCase(_player));
            else if (hasPlayer && totalPlayersOnThisPiece > 1)
                StartCoroutine(CheckForAlternatePlayerPosition(_player));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        totalPlayersOnThisPiece--;
        if (totalPlayersOnThisPiece == 0) hasPlayer = false;

        StopAllCoroutines();
    }



    private IEnumerator CheckForSpecialCase(Player _player)
    {
        yield return new WaitUntil(() => !_player.isPlayerMoving);

        if (hasJump)
        {
            print(_player.transform.localPosition + "<-from---Jump Case---to->" + BoardManager.Instance.piecesPositions[jumpToNumber - 1]);
            _player.JumpPlayerTo(jumpToNumber - 1);
            //_player.JumpPlayerTo(BoardManager.Instance.piecesPositions[jumpToNumber - 1]);
        }
        else
        {
            print("Picture Case");
            GameManager.currentPlayerInfo.isActivePlayer = false;
            //GameManager.Instance.audioManager.PlayPictureSound();

            GameManager.Instance.currentAvatarIndex = avatarIndex;
            UIManager.Instance.popupManager.ShowAvatarPopup();
            //yield return new WaitForSeconds(GameManager.Instance.audioManager.GetPictureClipLength(GameManager.Instance.currentAvatarIndex));
            //if (GameManager.Instance.isAvatarPopupOpen)
            //{
            //    UIManager.Instance.popupManager.HideAvatarPopup();
            //    yield return new WaitForSeconds(0.25f);
            //    GameManager.currentPlayerInfo.isActivePlayer = true;
            //    GameManager.Instance.currentAvatarIndex = -1;
            //}
        }

        yield return new WaitForSeconds(0.1f);
        if (hasPlayer && totalPlayersOnThisPiece > 1)
            StartCoroutine(CheckForAlternatePlayerPosition(_player));
    }

    private IEnumerator CheckForAlternatePlayerPosition(Player _player)
    {
        yield return new WaitUntil(() => !_player.isPlayerMoving);

        //print("New Position Case");
        _player.transform.localPosition = playerPositions[totalPlayersOnThisPiece - 1];
    }
}
