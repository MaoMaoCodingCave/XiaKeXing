using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTile : MonoBehaviour
{
    public bool canWalk = false;
    public bool inRange = false;
    private SpriteRenderer spriteRenderer;
    public LayerMask obstacleLayer;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CheckObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObstacle();
    }

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter()
    {
        // 放大1.2倍
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit()
    {
        // 恢复原始大小
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void CheckObstacle()
    {
        // 检查tile上是否有障碍物
        // 要记住Physics2D.OverlapCircle是检测两个collider，所以在上面的物体也要有collider
        Collider2D collider = Physics2D.OverlapCircle(transform.position, spriteRenderer.bounds.extents.x, obstacleLayer);
        if (collider != null)
        {
            canWalk = false;
        }
        else
        {
            canWalk = true;
        }
    }

    // 鼠标点击就移动
    private void OnMouseDown()
    {
        // 将这个tile的位置传给GameManager
        EventHandler.CallOnMovePlayer(transform);
    }
}
