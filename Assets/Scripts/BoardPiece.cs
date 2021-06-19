using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public int pieceNumber;
    public bool hasJump = false;
    public int jumpToNumber;
    public bool hasPicture = false;
    public bool isFirstPiece = false;
    public bool isLastPiece = false;

    public bool hasPlayer = false;
    [SerializeField] private int totalPlayersOnThisPiece = 0;

    void Start()
    {

    }

    public void SetPieceInfo()
    {

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            hasPlayer = true;
            totalPlayersOnThisPiece++;

            Player _player = collision.gameObject.GetComponent<Player>();

            if (isLastPiece)
            {
                print("Game Over");
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

        }
    }
}
