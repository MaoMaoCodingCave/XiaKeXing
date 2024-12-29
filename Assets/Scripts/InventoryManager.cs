using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Canvas inventoryCanvas;
    public GameObject inventoryPanel;
    public GameObject itemPrefab;
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

    public List<CommonItem> commonItems;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        inventoryCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
            if (inventoryCanvas.gameObject.activeSelf)
            {
                UpdateInventory();
            }
            else
            {
                foreach (Transform child in inventoryPanel.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    void UpdateInventory()
    {
        foreach (KeyValuePair<string, int> item in inventory)
        {
            // 每一个itemPrefab都有两个Text组件，用来显示物品的名字和数量
            Text[] texts = itemPrefab.GetComponentsInChildren<Text>();
            texts[0].text = item.Key;
            texts[1].text = item.Value.ToString();
            // 如果数量为0，则不生成prefab。
            if (item.Value > 0)
            {
                Instantiate(itemPrefab, inventoryPanel.transform);
            }
        }
    }
}
