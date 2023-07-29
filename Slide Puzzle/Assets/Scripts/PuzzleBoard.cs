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
    private int[,] winningArr;
    public int[,] currentArr;
    int grid = 1;
    private int emptyX;
    private int emptyY;

    public bool isMoving;
    public Transform movingPiece;

    private void Start()
    {
        ShufflePieces();
        InitiatePiecesPosition();
        GenerateWinningArray();
    }

    private void InitiatePiecesPosition()
    {
        Vector2 startPos = new Vector2((int)-(size.x / 2), (int)(size.y / 2));
        Vector2 currentPos = new Vector2(startPos.x, startPos.y);

        //Generate random empty space position
        Vector2 randomPos = new Vector2((int)Random.Range(1, size.x), (int)Random.Range(1, size.y));

        currentArr = new int[(int)size.x, (int)size.y];

        for(int i=1;i<=size.x;i++)
        {
            for(int j=1;j<=size.y;j++)
            {
                //empty one spot for sliding
                if (i == randomPos.x && j == randomPos.y)
                {
                    emptyPos = new Vector2(currentPos.x, currentPos.y);
                    currentPos.y -= 1;
                    currentArr[i-1, j-1] = 0;
                    emptyX = i - 1;
                    emptyY = j - 1;
                    continue;
                }

                GameObject piece = Instantiate(piecePrefab, transform);
                piece.transform.position = new Vector3(currentPos.x, currentPos.y, 0);
                currentPos.y -= 1;
                SetPieceImage(piece);

                currentArr[i-1, j-1] = piece.GetComponent<Piece>().pieceNumber;
                piece.GetComponent<Piece>().currentX = i - 1;
                piece.GetComponent<Piece>().currentY = j - 1;
            }

            currentPos.x += 1;
            currentPos.y = startPos.y;
        }
    }

    private void SetPieceImage(GameObject piece)
    {
        if(pieceCounter<puzzleAttribute.attributes.Count)
        {
            piece.GetComponent<SpriteRenderer>().sprite = puzzleAttribute.attributes[pieceCounter].image;
            piece.GetComponent<Piece>().pieceNumber = puzzleAttribute.attributes[pieceCounter].number;
            pieceCounter += 1;
        }

    }

    private void ShufflePieces()
    {
        //Knuth shuffle algorithm
        for(int i=0;i< puzzleAttribute.attributes.Count; i++)
        {
            var tmp = puzzleAttribute.attributes[i];
            int r = Random.Range(i, puzzleAttribute.attributes.Count);
            puzzleAttribute.attributes[i] = puzzleAttribute.attributes[r];
            puzzleAttribute.attributes[r] = tmp;
        }
    }

    private void GenerateWinningArray()
    {
        winningArr = new int[(int)size.x, (int)size.y];
        
        for(int i = 0;i<winningArr.GetLength(0);i++)
        {
            for(int j=0;j<winningArr.GetLength(1);j++)
            {
                //if this is the last row and column
                if(i == winningArr.GetLength(0)-1 && j == winningArr.GetLength(1)-1)
                {
                    winningArr[i, j] = 0;
                }
                else
                {
                    winningArr[i, j] = grid;
                    grid += 1;
                }
            }
        }
    }

    public void CheckWinState()
    {
        for(int i=0; i<size.x; i++)
        {
            for(int j=0;j<size.y;j++)
            {
                //if there is even one that does not match, then win conditions haven't been met.
                if(currentArr[i,j]!=winningArr[i,j])
                {
                    return;
                }
            }
        }

        //If all the same, then win.
        Debug.Log("WIN");
    }

    public void SwapPieceWithEmpty(ref int i, ref int j)
    {
        //Swap element value;
        var tmp = currentArr[i, j];
        currentArr[i, j] = 0;
        currentArr[emptyX, emptyY] = tmp;

        //Swap Coordinate
        int tmpX = i;
        int tmpY = j;

        i = emptyX;
        j = emptyY;

        emptyX = tmpX;
        emptyY = tmpY;
    }
}
