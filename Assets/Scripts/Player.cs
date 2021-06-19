using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public bool isPlayerMoving = false;
    public int currentPosition = 1;

    public int stepsCount = 1;
    public int moveTo = 1;


    private int _stepsRemainaing = 0;
    public int max = 100;



    void Start()
    {

    }

    void Update()
    {

    }

    [ContextMenu("MovePlayerTo")]
    public void MovePlayerTo()
    {
        if (currentPosition == moveTo || currentPosition > moveTo - 1)
            return;

        isPlayerMoving = true;
        transform.DOLocalMove(BoardManager.Instance.piecesPositions[++currentPosition], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
          {
              if (currentPosition == moveTo || currentPosition > moveTo - 1)
                  isPlayerMoving = false;
              else
                  MovePlayerTo();
          });
    }


    [ContextMenu("MovePlayerBySteps")]
    public void MovePlayerBySteps()
    {
        if (stepsCount == 0)
            return;

        isPlayerMoving = true;
        transform.DOLocalMove(BoardManager.Instance.piecesPositions[currentPosition], GameManager.Instance.playerMovementSpeed).SetEase(GameManager.Instance.playerEasyType).OnComplete(delegate
        {
            currentPosition++;
            if (--stepsCount > 0)
                MovePlayerBySteps();
            else
                isPlayerMoving = false;
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
            currentPosition = _number;
            isPlayerMoving = false;
        });
    }
}
