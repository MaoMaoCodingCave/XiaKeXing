using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private List<PlayerInBattle> players;
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            players.Add(other.GetComponent<PlayerInBattle>());
            EventHandler.CallOnBattleWithEnermy();
            // EventHandler.CallOnPassPlayerToBattleScene(players);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        players = new List<PlayerInBattle>();
        players.Add(this.GetComponent<PlayerInBattle>());
    }

    // Update is called once per frame
    void Update()
    {
        // 判断物体当前所在的场景是main场景还是battle场景
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "NewBattleScene")
        {
            this.GetComponent<EnermyInfo>().enabled = true;
        }
        else
        {
            this.GetComponent<EnermyInfo>().enabled = false;
        }
    }
}
