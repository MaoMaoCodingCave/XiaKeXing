using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyInfo : MonoBehaviour
{
    public int enermyHp;
    // private float testTimer;
    // private int enermyAttack;
    // Start is called before the first frame update
    void Start()
    {
        enermyHp = 100;
        // testTimer = 1f;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnPlayerAttack += TakeDamage;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnPlayerAttack -= TakeDamage;
    }

    private void TakeDamage(int playerAttack)
    {
        enermyHp -= playerAttack;
    }

    // Update is called once per frame
    void Update()
    {
        // 只是用来测试
        // testTimer -= Time.deltaTime;
        // if (testTimer <= 0)
        // {
        //     testTimer = 1f;
        //     enermyHp -= 20;
        // }

        if (enermyHp == 0)
        {
            // 呼叫敌人死亡事件，切换回主场景，然后删除敌人。
            EventHandler.CallOnEnermyDead();
        }
    }
}
