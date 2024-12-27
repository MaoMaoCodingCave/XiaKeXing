using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SingleTile> tiles;
    public List<PlayerInBattle> players;
    private PlayerInBattle currentPlayer;
    private float speed = 5f;
    private int playerTurn;
    private bool isMoving = false;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnMovePlayer += OnMovePlayer;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnMovePlayer -= OnMovePlayer;
    }

    private void OnMovePlayer(Transform transform)
    {
        if (currentPlayer != null)
        {
            if (transform.GetComponent<SingleTile>().canWalk && transform.GetComponent<SingleTile>().inRange)
            {
                MovePlayer(transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 0;
        currentPlayer = players[playerTurn];
        ShowCanMoveTile(currentPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn >= players.Count)
        {
            playerTurn = 0;
        }
        if (!isMoving)
        {
            currentPlayer = players[playerTurn];
            ShowCanMoveTile(currentPlayer);
        }
    }

    void MovePlayer(Transform tilePosition)
    {
        StartCoroutine(CoMovePlayer(tilePosition));
    }

    IEnumerator CoMovePlayer(Transform tilePosition)
    {
        isMoving = true;
        // 先进行横向移动
        while (currentPlayer.transform.position.x != tilePosition.position.x)
        {
            // 根据目标位置的x坐标，判断角色的朝向
            if (currentPlayer.transform.position.x > tilePosition.position.x)
            {
                currentPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                currentPlayer.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, new Vector3(tilePosition.position.x, currentPlayer.transform.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        // 再进行纵向移动
        while (currentPlayer.transform.position.y != tilePosition.position.y)
        {
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, new Vector3(currentPlayer.transform.position.x, tilePosition.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        // 移动结束，isMoving设置为false
        isMoving = false;
        // 移动结束，再次到当前角色回合
        playerTurn++;
        // 移动结束后，重置所有tile的颜色
        ResetTilesColor();
    }

    void ResetTilesColor()
    {
        foreach (var tile in tiles)
        {
            Color color = Color.white;
            color.a = 100f / 255f;
            tile.GetComponent<SpriteRenderer>().color = color;
            tile.inRange = false;
        }
    }


    void ShowCanMoveTile(PlayerInBattle player)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            float disX = Mathf.Abs(tiles[i].transform.position.x - player.transform.position.x);
            float disY = Mathf.Abs(tiles[i].transform.position.y - player.transform.position.y);
            // 如果x+y在3格以内
            if (disX + disY <= player.moveRange && tiles[i].canWalk)
            {
                tiles[i].GetComponent<SpriteRenderer>().color = Color.green;
                tiles[i].inRange = true;
            }
        }
    }
}
