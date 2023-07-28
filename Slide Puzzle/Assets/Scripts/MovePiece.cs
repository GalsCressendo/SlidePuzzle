using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    private PuzzleBoard board;
    private float speed = 1.5f;
    private float t = 0f;

    private Vector2 source = new Vector2();
    private Vector2 destination = new Vector2();

    private void Awake()
    {
        board = transform.parent.GetComponent<PuzzleBoard>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!board.isMoving) //check if we are currently not moving any piece
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform == transform)
                        {
                            t = 0f;
                            //Check if empty position is near this gameobject
                            Vector2 targetPos = transform.position;
                            bool ableToMove = (targetPos.x - 1 == board.emptyPos.x || targetPos.x + 1 == board.emptyPos.x) && targetPos.y == board.emptyPos.y ||
                                (targetPos.y - 1 == board.emptyPos.y || targetPos.y + 1 == board.emptyPos.y) && targetPos.x == board.emptyPos.x;

                            if(ableToMove)
                            {
                                board.isMoving = ableToMove;
                                board.movingPiece = transform;
                                source = transform.position;
                                destination = board.emptyPos;
                                board.emptyPos = transform.position;
                            }

                        }

                    }
                }
            }
            
        }

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
        }
    }
}
