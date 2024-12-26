using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int movementRange = 3;
    private float speed = 3f;
    private float timer = 5f;
    public bool hasMoved = false;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        BattleManager.instance.selectedUnit = this;
        ShowWalkableTiles();
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    // void OnMouseDown()
    // {
    //     BattleManager.instance.selectedUnit = this;
    //     // Debug.Log("Unit: " + transform.position);
    //     ShowWalkableTiles();
    // }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (hasMoved)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                hasMoved = false;
                ShowWalkableTiles();
                timer = 5f;
            }
        }
    }

    private void ShowWalkableTiles()
    {
        if (hasMoved)
        {
            return;
        }
        // 显示可行走的tile
        // 1. 获取当前unit所在的tile
        // 2. 获取当前unit的移动范围
        // 3. 遍历tile，显示可行走的tile
        for (int i = 0; i < BattleManager.instance.tiles.Count; i++)
        {
            float distanceX = Mathf.Abs(transform.position.x - BattleManager.instance.tiles[i].transform.position.x);
            float distanceY = Mathf.Abs(transform.position.y - BattleManager.instance.tiles[i].transform.position.y);
            if (distanceX + distanceY <= movementRange)
            {
                if (BattleManager.instance.tiles[i].GetComponent<Tile>().isWalkable)
                {
                    BattleManager.instance.tiles[i].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
    }

    public void Move(Transform trans)
    {
        StartCoroutine(CoMove(trans));
    }

    IEnumerator CoMove(Transform trans)
    {
        // 先进行横向移动
        while (transform.position.x != trans.position.x)
        {
            // 根据目标位置的x坐标，判断角色的朝向
            if (transform.position.x > trans.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(trans.position.x, transform.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        // 再进行纵向移动
        while (transform.position.y != trans.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, trans.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
        // 移动结束后，重置所有tile的颜色
        RestAllTiles();
    }

    private void RestAllTiles()
    {
        for (int i = 0; i < BattleManager.instance.tiles.Count; i++)
        {
            Color color = Color.white;
            color.a = 100f / 255f;
            BattleManager.instance.tiles[i].GetComponent<SpriteRenderer>().color = color;
        }
    }
}
