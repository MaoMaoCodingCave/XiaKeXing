using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCommonItem", menuName = "CommonItem")]
public class CommonItem : ScriptableObject
{
    public Sprite itemImageSprite;
    public string itemName;
}
