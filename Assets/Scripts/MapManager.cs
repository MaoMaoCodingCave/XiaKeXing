using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Canvas mapCanvas;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnCloseMapCanvas += CloseMapCanvas;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnCloseMapCanvas -= CloseMapCanvas;
    }

    private void CloseMapCanvas()
    {
        mapCanvas.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        mapCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapCanvas.gameObject.SetActive(!mapCanvas.gameObject.activeSelf);
        }
    }
}
