using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject piecePrefab;

    [Header("Size")]
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    private void Start()
    {
        InitiatePiecesPosition();
    }

    private void InitiatePiecesPosition()
    {
        int startPosX = -(sizeX/2);
        int startPosY = (sizeY/2);
        int posX = startPosX;
        int posY = startPosY;

        for(int i=1;i<=sizeX;i++)
        {
            for(int j=1;j<=sizeY;j++)
            {
                //the last piece need to be empty for sliding space
                if (i == sizeX && j == sizeY)
                {
                    return;
                }

                GameObject piece = Instantiate(piecePrefab, transform);
                piece.transform.position = new Vector3(posX, posY, 0);
                posY -= 1;
            }

            posX += 1;
            posY = startPosY;
        }
    }
}
