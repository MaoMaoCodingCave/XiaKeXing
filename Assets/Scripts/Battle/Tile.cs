using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 如果需要让tile随机生成，可以在这里定义一个变量，然后在生成tile的时候赋值
    private SpriteRenderer spriteRenderer;
    public LayerMask obstacleLayer;
    // [SerializeField] private Sprite[] tileSprites;
    public bool isWalkable;
    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sprite = tileSprites[Random.Range(0, tileSprites.Length)];
        CheckObstacle();
    }

    private void OnMouseEnter()
    {
        // 鼠标移入tile时，显示tile的信息
        // Debug.Log("Tile: " + transform.position);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    private void OnMouseExit()
    {
        // 鼠标移出tile时，清空信息
        // Debug.Log("");
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CheckObstacle();
    }

    private void CheckObstacle()
    {
        // 检查tile上是否有障碍物
        // 要记住Physics2D.OverlapCircle是检测两个collider，所以在上面的物体也要有collider
        Collider2D collider = Physics2D.OverlapCircle(transform.position, spriteRenderer.bounds.extents.x, obstacleLayer);
        if (collider != null)
        {
            isWalkable = false;
        }
        else
        {
            isWalkable = true;
        }
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if (isWalkable && BattleManager.instance.selectedUnit != null && !BattleManager.instance.selectedUnit.hasMoved && inRange)
        {
            // Debug.Log("Tile: " + transform.position);
            BattleManager.instance.selectedUnit.Move(transform);
            // CheckObstacle();
        }
    }
}
