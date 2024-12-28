using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler
{
    public static event Action<Transform> OnMovePlayer;
    public static void CallOnMovePlayer(Transform tilePosition)
    {
        OnMovePlayer?.Invoke(tilePosition);
    }

    public static event Action<Vector3> OnDigTile;
    public static void CallOnDigTile(Vector3 tilePosition)
    {
        OnDigTile?.Invoke(tilePosition);
    }
}
