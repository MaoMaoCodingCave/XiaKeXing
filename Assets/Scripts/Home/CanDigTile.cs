using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDigTile : MonoBehaviour
{
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EventHandler.CallOnDigTile(mousepos);
    }
}
