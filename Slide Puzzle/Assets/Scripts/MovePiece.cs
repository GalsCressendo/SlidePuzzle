using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    private PuzzleBoard board;

    private void Awake()
    {
        board = transform.parent.GetComponent<PuzzleBoard>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.transform!=null)
                {
                    if(hit.transform == transform)
                    {
                        //Check if empty position is near this gameobject
                        Vector2 targetPos = transform.position;
                        if ((targetPos.x - 1 == board.emptyPos.x || targetPos.x + 1 == board.emptyPos.x) && targetPos.y == board.emptyPos.y)
                        {
                            Debug.Log("Yeah I am near in X");
                        }
                        else if ((targetPos.y - 1 == board.emptyPos.y || targetPos.y + 1 == board.emptyPos.y) && targetPos.x == board.emptyPos.x)
                        {
                            Debug.Log("Yeah I am near in Y");
                        }
                    }

                }
            }
        }
    }
}
