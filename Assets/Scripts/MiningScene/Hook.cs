using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float swingSpeed = 2f; // 钩子摆动速度
    public float extendSpeed = 5f; // 钩子伸出速度
    public float retractSpeed = 5f; // 钩子收回速度
    public float maxDistance = 100f; // 钩子最大伸出距离

    private bool isExtending = false;
    private bool isRetracting = false;
    private Vector3 startPosition;
    private Transform caughtItem;
    public Transform caughtItemTransform;
    private LineRenderer lineRenderer;

    // /// <summary>
    // /// This function is called when the object becomes enabled and active.
    // /// </summary>
    // void OnEnable()
    // {
    //     EventHandler.OnDragItemEvent += OnDragItemEvent;
    //     EventHandler.OnDragItemFinished += OnDragItemFinishedEvent;
    // }

    // /// <summary>
    // /// This function is called when the behaviour becomes disabled or inactive.
    // /// </summary>
    // void OnDisable()
    // {
    //     EventHandler.OnDragItemEvent -= OnDragItemEvent;
    //     EventHandler.OnDragItemFinished -= OnDragItemFinishedEvent;
    // }

    // private void OnDragItemFinishedEvent()
    // {
    //     retractSpeed = 5f;
    // }

    // private void OnDragItemEvent(float weight)
    // {
    //     isExtending = false;
    //     isRetracting = true;
    //     retractSpeed = weight;
    // }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isExtending && !isRetracting)
        {
            // 钩子摆动
            float angle = Mathf.Sin(Time.time * swingSpeed) * 75f; // 角度摆动
            transform.rotation = Quaternion.Euler(0, 0, angle-180);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isExtending && !isRetracting)
        {
            isExtending = true;
        }

        if (isExtending)
        {
            transform.position += transform.up * extendSpeed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
            {
                isExtending = false;
                isRetracting = true;
            }
        }

        if (isRetracting)
        {
            Vector3 hookTarget = Vector3.MoveTowards(transform.position, startPosition, retractSpeed * Time.deltaTime);
            transform.position = hookTarget;

            if (caughtItem != null)
            {
            //     Vector3 caughtItemTarget = Vector3.MoveTowards(caughtItem.position, startPosition, retractSpeed * Time.deltaTime);
                caughtItem.position = caughtItemTransform.position;
            }

            if (Vector3.Distance(startPosition, transform.position) <= 0.1f)
            {
                isRetracting = false;
                // 检查捕捉物品是否为空，不为空就销毁该物品。
                if (caughtItem != null)
                {
                    int itemScore = caughtItem.GetComponent<MiningCollectItem>().score;
                    EventHandler.CallOnDragItemFinished(itemScore);
                    Destroy(caughtItem.gameObject);
                    caughtItem = null;
                }
                retractSpeed = 5f;
            }
        }
        // 更新 LineRenderer 的两端点
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, transform.position);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MiningCollectItem"))
        {
            caughtItem = other.transform;
            isExtending = false;
            isRetracting = true;
            float itemWeight = other.GetComponent<MiningCollectItem>().weight;
            retractSpeed /= itemWeight;
        }
    }
}
