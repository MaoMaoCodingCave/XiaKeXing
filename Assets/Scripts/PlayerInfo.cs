using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int hp;
    private int attack;
    private bool isUp;
    private bool isDown;
    private bool isLeft;
    private bool isRight;
    // private int defense;
    // private int speed;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        attack = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        Vector3 attackDirection = GetMouseDirection();
        EventHandler.CallOnGetAttackDirection();
        // EventHandler.CallOnPlayerAttack(attack);
    }

    // 根据鼠标位置判断朝向
    // 只让一个方向是true，根据偏向哪个方向最多。
    private Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = this.transform.position;
        Vector3 direction = mousePosition - playerPosition;
        Vector3 dir = Vector3.zero;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                dir = Vector3.right;
            }
            else
            {
                dir = Vector3.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                dir = Vector3.up;
            }
            else
            {
                dir = Vector3.down;
            }
        }
        return dir;
    }
}
