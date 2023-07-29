using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    [SerializeField]private GameObject piecePrefab;

    [Header("Attributes")]
    [SerializeField] private Vector2 size;
    public Vector2 emptyPos;
    [SerializeField]PuzzleAttribute puzzleAttribute;
    private int pieceCounter = 0;

    public bool isMoving;
    public Transform movingPiece;

    private void Start()
    {
        ShufflePieces();
        InitiatePiecesPosition();
    }

    private void InitiatePiecesPosition()
    {
        Vector2 startPos = new Vector2((int)-(size.x / 2), (int)(size.y / 2));
        Vector2 currentPos = new Vector2(startPos.x, startPos.y);

        //Generate random empty space position
        Vector2 randomPos = new Vector2((int)Random.Range(1, size.x), (int)Random.Range(1, size.y));

        for(int i=1;i<=size.x;i++)
        {
            for(int j=1;j<=size.y;j++)
            {
                //empty one spot for sliding
                if (i == randomPos.x && j == randomPos.y)
                {
                    emptyPos = new Vector2(currentPos.x, currentPos.y);
                    currentPos.y -= 1;
                    continue;
                }

                GameObject piece = Instantiate(piecePrefab, transform);
                piece.transform.position = new Vector3(currentPos.x, currentPos.y, 0);
                currentPos.y -= 1;
                SetPieceImage(piece.GetComponent<SpriteRenderer>());
            }

            currentPos.x += 1;
            currentPos.y = startPos.y;
        }
    }

    private void SetPieceImage(SpriteRenderer renderer)
    {
        if(pieceCounter<puzzleAttribute.pieces.Count)
        {
            renderer.sprite = puzzleAttribute.pieces[pieceCounter];
            pieceCounter += 1;
        }

    }

    private void ShufflePieces()
    {
        //Knuth shuffle algorithm
        for(int i=0;i<puzzleAttribute.pieces.Count;i++)
        {
            Sprite tmp = puzzleAttribute.pieces[i];
            int r = Random.Range(i, puzzleAttribute.pieces.Count);
            puzzleAttribute.pieces[i] = puzzleAttribute.pieces[r];
            puzzleAttribute.pieces[r] = tmp;
        }
    }
}
