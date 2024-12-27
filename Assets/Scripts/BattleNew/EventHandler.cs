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
}
