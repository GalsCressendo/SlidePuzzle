using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject piecePrefab;

    [Header("Size")]
    [SerializeField] private Vector2 size;


    private void Start()
    {
        InitiatePiecesPosition();
    }

    private void InitiatePiecesPosition()
    {
        Vector2 startPos = new Vector2(-(size.x / 2), (size.y / 2));
        Vector2 currentPos = new Vector2(startPos.x, startPos.y);

        for(int i=1;i<=size.x;i++)
        {
            for(int j=1;j<=size.y;j++)
            {
                //the last piece need to be empty for sliding space
                if (i == size.x && j == size.y)
                {
                    return;
                }

                GameObject piece = Instantiate(piecePrefab, transform);
                piece.transform.position = new Vector3(currentPos.x, currentPos.y, 0);
                currentPos.y -= 1;
            }

            currentPos.x += 1;
            currentPos.y = startPos.y;
        }
    }
}
