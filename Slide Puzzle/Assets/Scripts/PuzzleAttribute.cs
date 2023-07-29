using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleAttribute", menuName = "Attribute/PuzzleAttribute", order = 1)]
public class PuzzleAttribute : ScriptableObject
{
    public string PuzzleName;

    [System.Serializable]
    public class Attribute
    {
        public int number;
        public Sprite image;
    }

    public List<Attribute> attributes;
}
