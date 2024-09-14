using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = false; // 角色初始朝向为左

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 获取水平和垂直输入 (WASD 或方向键)
        float moveX = Input.GetAxisRaw("Horizontal");

        // 将输入转换为移动向量
        movement = new Vector2(moveX, 0f).normalized;

        // 翻转角色朝向
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // 通过刚体2D来移动物体
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}