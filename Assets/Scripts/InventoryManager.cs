using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>()
    {
        {"Noodles", 5},
        {"Sauce", 2},
        {"Meat", 3},
        {"Cheese", 1},
        {"Salt", 4},
        {"Peanut", 3},
        {"Pepper", 5},
    };
}
