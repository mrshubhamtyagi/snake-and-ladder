using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardPiecePrefab;
    public int totalPieces;
    public int[] picturesNumbers;
    public Vector2[] jumpNumbers; // x: piece number, y: to jump piece number
    public Vector2[] piecesPositions;

    void Start()
    {
        InitializePieces();
    }

    private void InitializePieces()
    {
        BoardPiece _piece;
        for (int i = 0; i < totalPieces; i++)
        {
            _piece = Instantiate(boardPiecePrefab, transform).GetComponent<BoardPiece>();
            _piece.gameObject.name = "Piece_" + (i + 1);
            _piece.pieceNumber = i + 1;

            // Position
            _piece.transform.localPosition = piecesPositions[i];

            // First or Last
            if (i == 0) _piece.isFirstPiece = true;
            else if (i == totalPieces - 1) _piece.isLastPiece = true;
            else
            {
                _piece.isFirstPiece = _piece.isLastPiece = false;

            }

            // Jump Details
            foreach (var item in jumpNumbers)
            {
                if (i+1 == item.x)
                {
                    _piece.hasJump = true;
                    _piece.jumpToNumber = (int)item.y;
                    break;
                }
                else
                {
                    _piece.hasJump = false;
                    _piece.jumpToNumber = 0;
                }
            }

            // Picture
            foreach (var item in picturesNumbers)
            {
                if (i+1 == item)
                {
                    _piece.hasPicture = true;
                    break;
                }
                else
                    _piece.hasPicture = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}


[System.Serializable]
public class SpecialPieceInfo
{
    public string pieceName;
    public AudioClip audioClip;

}
