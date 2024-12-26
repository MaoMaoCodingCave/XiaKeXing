using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbItem : MonoBehaviour
{
    public GameObject interactText;
    public Canvas harvestPage;
    private bool canHarvest = false;
    // Start is called before the first frame update
    void Start()
    {
        interactText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHarvest)
        {
            harvestPage.gameObject.SetActive(!harvestPage.gameObject.activeSelf);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            canHarvest = true;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            canHarvest = false;
        }
    }
}
