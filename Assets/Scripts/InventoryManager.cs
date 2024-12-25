using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>()
    {
        {"面条", 5},
        {"酱料", 2},
        {"肉", 3},
        {"起司", 1},
        {"盐", 4},
        {"花生", 3},
        {"辣椒", 5},
        {"苹果", 3},
        {"桃子", 2},
        {"红薯", 4}
    };
}
