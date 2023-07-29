using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private PuzzleBoard board;
    private float speed = 2f;
    private float t = 0f;

    private Vector2 source = new Vector2();
    private Vector2 destination = new Vector2();

    public int currentX;
    public int currentY;

    //Attribute
    public int pieceNumber;

    private void Awake()
    {
        board = transform.parent.GetComponent<PuzzleBoard>();
    }

    private void OnMouseDown()
    {
        if(!board.isMoving)
        {
            t = 0;

            //Check if empty position is near this gameobject
            Vector2 targetPos = transform.position;
            bool ableToMove = (targetPos.x - 1 == board.emptyPos.x || targetPos.x + 1 == board.emptyPos.x) && targetPos.y == board.emptyPos.y ||
                (targetPos.y - 1 == board.emptyPos.y || targetPos.y + 1 == board.emptyPos.y) && targetPos.x == board.emptyPos.x;

            if (ableToMove)
            {
                board.isMoving = ableToMove;
                board.movingPiece = transform;
                source = transform.position;
                destination = board.emptyPos;
                board.emptyPos = transform.position;
            }
        }
    }

    private void Update()
    {
        if(board.isMoving && board.movingPiece == transform)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(source, destination, t);
        t += Time.deltaTime * speed;
        if (transform.position == new Vector3(destination.x, destination.y, 0))
        {
            board.isMoving = false;
            board.movingPiece = null;

            board.SwapPieceWithEmpty(ref currentX, ref currentY);
            board.CheckWinState();
        }
    }
}
