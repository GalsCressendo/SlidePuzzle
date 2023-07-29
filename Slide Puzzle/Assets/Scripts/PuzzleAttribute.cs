using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleAttribute", menuName = "Attribute/PuzzleAttribute", order = 1)]
public class PuzzleAttribute : ScriptableObject
{
    public string PuzzleName;
    public List<Sprite> pieces;
}
