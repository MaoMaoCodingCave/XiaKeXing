using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CanDigTilemap : MonoBehaviour
{
    public Tilemap candigTilemap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            // 将鼠标点击位置从屏幕坐标转换到世界坐标
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, 
                Input.mousePosition.y, 
                -Camera.main.transform.position.z // 使用相机的z轴深度
            ));

            // 修正 Z 轴值为 0，与 Tilemap 的 Z 对齐
            worldPoint.z = 0;

            // 转换为 Grid 坐标
            Vector3Int gridPosition = candigTilemap.WorldToCell(worldPoint);
            TileBase clickedTile = candigTilemap.GetTile(gridPosition);

            if (clickedTile != null)
            {
                Debug.Log($"Tile found at {gridPosition}: {clickedTile.name}");
                candigTilemap.SetTile(gridPosition, null); // 移除 Tile
            }
            else
            {
                Debug.Log($"No Tile at {gridPosition}");
            }
        }
    }
}
