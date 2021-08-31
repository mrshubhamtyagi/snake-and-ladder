using System.Collections;
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

    void Start()
    {
        SetPieceInfo();
    }

    public void SetPieceInfo()
    {
        pieceNumber = transform.GetSiblingIndex() + 1;
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
            {
                StartCoroutine(CheckForSpecialCase(_player));
            }
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
            print("Jump Case");
            _player.JumpPlayerTo(jumpToNumber - 1);
        }
        else
        {
            print("Picture Case");
            GameManager.currentPlayerInfo.isActivePlayer = false;
            GameManager.Instance.audioManager.PlayPictureSound();

            GameManager.Instance.currentAvatarIndex = avatarIndex;
            UIManager.Instance.popupManager.ShowAvatarPopup();
            yield return new WaitForSeconds(3);
            UIManager.Instance.popupManager.HideAvatarPopup();
            yield return new WaitForSeconds(0.25f);
            GameManager.currentPlayerInfo.isActivePlayer = true;
            GameManager.Instance.currentAvatarIndex = -1;
        }
    }

}
