using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiningCollectItem : MonoBehaviour
{
    public int score = 0; // 分数
    public float weight; // 拖拽速度调整

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Hook"))
    //     {
    //         // collision.GetComponent<Hook>().enabled = false; // 暂停钩子摆动
    //         EventHandler.CallOnDragItemEvent(weight); // 通知拖拽物品
    //         StartCoroutine(DragItem(collision.transform, playerTransform)); // 拖拽物品
    //     }
    // }

    // private IEnumerator DragItem(Transform hook, Transform pos)
    // {
    //     // 拖拽物品到玩家位置
    //     while (Vector3.Distance(transform.position, pos.position) > 0.1f)
    //     {
    //         transform.position = Vector3.MoveTowards(transform.position, pos.position, Time.deltaTime / weight);
    //         yield return null;
    //     }

    //     // 返回起始位置并加分
    //     Destroy(gameObject); // 摧毁物品
    //     EventHandler.CallOnDragItemFinished(); // 通知拖拽物品完成
    //     // hook.GetComponent<Hook>().enabled = true; // 恢复钩子摆动
    // }
}
