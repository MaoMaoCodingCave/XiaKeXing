using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EventHandler
{
    public static event Action<Transform> OnMovePlayer;
    public static void CallOnMovePlayer(Transform tilePosition)
    {
        OnMovePlayer?.Invoke(tilePosition);
    }

    public static event Action<UnityEngine.Vector3> OnDigTile;
    public static void CallOnDigTile(UnityEngine.Vector3 tilePosition)
    {
        OnDigTile?.Invoke(tilePosition);
    }

    public static event Action OnBattleWithEnermy;
    public static void CallOnBattleWithEnermy()
    {
        OnBattleWithEnermy?.Invoke();
    }

    public static event Action OnEnermyDead;
    public static void CallOnEnermyDead()
    {
        OnEnermyDead?.Invoke();
    }

    public static event Action OnPlayerWin;
    public static void CallOnPlayerWin()
    {
        OnPlayerWin?.Invoke();
    }

    public static event Action<int> OnPlayerAttack;
    public static void CallOnPlayerAttack(int attack)
    {
        OnPlayerAttack?.Invoke(attack);
    }

    public static event Action OnGetAttackDirection;
    public static void CallOnGetAttackDirection()
    {
        OnGetAttackDirection?.Invoke();
    }


//     public static event Action<List<PlayerInBattle>> OnPassPlayerToBattleScene;
//     public static void CallOnPassPlayerToBattleScene(List<PlayerInBattle> players)
//     {
//         OnPassPlayerToBattleScene?.Invoke(players);
//     }
// }
}
