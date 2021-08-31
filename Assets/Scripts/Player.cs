using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public bool isPlayerMoving = false;
    //public int currentPosition = 1;

    //public int stepsCount = 1;
    //public int moveTo = 1;

    public int currentPieceNumber;
    public bool hasPlayerStarted = false;

    //private int _stepsRemainaing = 0;
    //public int max = 100;



    void Start()
    {
        currentPieceNumber = 0;
    }

    //[ContextMenu("MovePlayerTo")]
    //public void MovePlayerTo()
    //{
    //    if (currentPosition == moveTo || currentPosition > moveTo - 1)
    //        return;

    //    isPlayerMoving = true;
    //    transform.DOLocalMove(BoardManager.Instance.piecesPositions[++currentPosition], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
    //      {
    //          if (currentPosition == moveTo || currentPosition > moveTo - 1)
    //              isPlayerMoving = false;
    //          else
    //              MovePlayerTo();
    //      });
    //}


    //[ContextMenu("MovePlayerBySteps")]
    //public void MovePlayerBySteps()
    //{
    //    if (stepsCount == 0)
    //        return;

    //    //if (!hasPlayerStarted)
    //    //{
    //    //    hasPlayerStarted = true;
    //    //    stepsCount++;
    //    //}

    //    isPlayerMoving = true;
    //    transform.DOLocalMove(BoardManager.Instance.piecesPositions[currentPosition], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
    //    {
    //        currentPosition++;
    //        if (--stepsCount > 0)
    //            MovePlayerBySteps();
    //        else
    //        {
    //            playerInfo.OnPlayerTurnOver();
    //            isPlayerMoving = false;
    //        }
    //    });
    //}


    public void MovePlayerBySteps(int _steps)
    {
        print("MovePlayerBySteps");

        isPlayerMoving = true;
        transform.DOLocalMove(BoardManager.Instance.piecesPositions[currentPieceNumber], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
        {
            //currentPosition++;
            if (--_steps > 0)
                MovePlayerBySteps(_steps);
            else
            {
                playerInfo.OnPlayerTurnOver();
                isPlayerMoving = false;
            }
        });
    }

    public void JumpPlayerTo(Vector2 _position)
    {
        transform.DOLocalMove(_position, 1);
    }

    public void JumpPlayerTo(int _number)
    {
        isPlayerMoving = true;
        transform.DOLocalMove(BoardManager.Instance.piecesPositions[_number], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
        {
            //currentPosition = _number;
            isPlayerMoving = false;
        });
    }
}
