using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "buildingSpritesData", menuName = "buildings_Data")]
public class SpriteData : ScriptableObject 
{
    public Sprite[] buildingSprites;
}
